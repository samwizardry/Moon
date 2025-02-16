using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

using ImGuiNET;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Moon;

// TODO: move shader creation to common Shader class
// TODO: labelObject gl debug
public class ImGuiLayer : Layer, IDisposable
{
    private Vector2 _scaleFactor = Vector2.One;

    private int _vertexArray;
    private int _vertexBuffer;
    private int _vertexBufferSize;
    private int _indexBuffer;
    private int _indexBufferSize;
    private int _fontTexture;
    private int _shader;
    private int _fontTextureUniformLocation;
    private int _projectionMatrixUniformLocation;

    private readonly bool _KHRDebugAvailable;
    private readonly int _glVersion;
    private readonly bool _compatibilityProfile;

    public ImGuiLayer()
        : base("ImgGuiLayer")
    {
        int major = GL.GetInteger(GetPName.MajorVersion);
        int minor = GL.GetInteger(GetPName.MinorVersion);

        _glVersion = major * 100 + minor * 10;

        _KHRDebugAvailable = (major == 4 && minor >= 3) || IsExtensionSupported("KHR_debug");

        _compatibilityProfile = (GL.GetInteger((GetPName)All.ContextProfileMask) & (int)All.ContextCompatibilityProfileBit) != 0;

        var context = ImGui.CreateContext();
        ImGui.SetCurrentContext(context);
        ImGui.StyleColorsDark();

        var io = ImGui.GetIO();

        io.Fonts.AddFontDefault();

        io.BackendFlags |= ImGuiBackendFlags.HasMouseCursors | ImGuiBackendFlags.HasSetMousePos | ImGuiBackendFlags.RendererHasVtxOffset;
        io.ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard | ImGuiConfigFlags.DockingEnable;
        io.Fonts.Flags |= ImFontAtlasFlags.NoBakedLines;

        CreateDeviceResources();
    }

    public void Dispose()
    {
        GL.DeleteVertexArray(_vertexArray);
        GL.DeleteBuffer(_vertexBuffer);
        GL.DeleteBuffer(_indexBuffer);
        GL.DeleteTexture(_fontTexture);
        GL.DeleteProgram(_shader);
    }

    public override void OnAttach(Game game)
    {
        base.OnAttach(game);
    }

    public override void OnDetach()
    {
        base.OnDetach();
    }

    public override void OnUpdate(FrameEventArgs args)
    {
        var io = ImGui.GetIO();
        var clientSize = _game!.ClientSize;
        io.DisplaySize = new Vector2(clientSize.X / _scaleFactor.X, clientSize.Y / _scaleFactor.Y);
        io.DisplayFramebufferScale = _scaleFactor;
        io.DeltaTime = (float)_game.UpdateTime;
    }

    public override void OnRender(FrameEventArgs args)
    {
        ImGui.NewFrame();

        ImGui.DockSpaceOverViewport();
        ImGui.ShowDemoWindow();

        ImGui.Render();
        RenderImDrawData(ImGui.GetDrawData());

        CheckGLError("End of frame");
    }

    #region Input events

    public override bool OnMouseDown(MouseButtonEventArgs e)
    {
        ImGuiIOPtr io = ImGui.GetIO();
        int button = (int)e.Button;

        if (button >= 0 && button < io.MouseDown.Count)
        {
            io.MouseDown[button] = true;
        }

        return false;
    }

    public override bool OnMouseUp(MouseButtonEventArgs e)
    {
        ImGuiIOPtr io = ImGui.GetIO();
        int button = (int)e.Button;

        if (button >= 0 && button < io.MouseDown.Count)
        {
            io.MouseDown[button] = false;
        }

        return false;
    }

    public override bool OnMouseMove(MouseMoveEventArgs e)
    {
        ImGuiIOPtr io = ImGui.GetIO();
        io.MousePos = new Vector2(e.X, e.Y);

        return false;
    }

    public override bool OnMouseWheel(MouseWheelEventArgs e)
    {
        ImGuiIOPtr io = ImGui.GetIO();
        io.MouseWheel += e.OffsetY;
        io.MouseWheelH += e.OffsetX;

        return false;
    }

    public override bool OnKeyDown(KeyboardKeyEventArgs e)
    {
        ImGuiIOPtr io = ImGui.GetIO();
        io.AddKeyEvent(TranslateKey(e.Key), true);

        io.KeyCtrl = e.Key == Keys.LeftControl || e.Key == Keys.RightControl;
        io.KeyAlt = e.Key == Keys.LeftAlt || e.Key == Keys.RightAlt;
        io.KeyShift = e.Key == Keys.LeftShift || e.Key == Keys.RightShift;
        io.KeySuper = e.Key == Keys.LeftSuper || e.Key == Keys.RightSuper;

        return false;
    }

    public override bool OnKeyUp(KeyboardKeyEventArgs e)
    {
        ImGuiIOPtr io = ImGui.GetIO();
        io.AddKeyEvent(TranslateKey(e.Key), false);

        return false;
    }

    public override bool OnTextInput(TextInputEventArgs e)
    {
        ImGuiIOPtr io = ImGui.GetIO();

        if (e.Unicode >= 0 && e.Unicode < 0x10000)
            io.AddInputCharacter((uint)e.Unicode);

        return false;
    }

    #endregion

    #region ImGui specific

    private void RenderImDrawData(ImDrawDataPtr drawData)
    {
        if (drawData.CmdListsCount == 0)
        {
            return;
        }

        // Get intial state.
        int prevVAO = GL.GetInteger(GetPName.VertexArrayBinding);
        int prevArrayBuffer = GL.GetInteger(GetPName.ArrayBufferBinding);
        int prevProgram = GL.GetInteger(GetPName.CurrentProgram);
        bool prevBlendEnabled = GL.GetBoolean(GetPName.Blend);
        bool prevScissorTestEnabled = GL.GetBoolean(GetPName.ScissorTest);
        int prevBlendEquationRgb = GL.GetInteger(GetPName.BlendEquationRgb);
        int prevBlendEquationAlpha = GL.GetInteger(GetPName.BlendEquationAlpha);
        int prevBlendFuncSrcRgb = GL.GetInteger(GetPName.BlendSrcRgb);
        int prevBlendFuncSrcAlpha = GL.GetInteger(GetPName.BlendSrcAlpha);
        int prevBlendFuncDstRgb = GL.GetInteger(GetPName.BlendDstRgb);
        int prevBlendFuncDstAlpha = GL.GetInteger(GetPName.BlendDstAlpha);
        bool prevCullFaceEnabled = GL.GetBoolean(GetPName.CullFace);
        bool prevDepthTestEnabled = GL.GetBoolean(GetPName.DepthTest);
        int prevActiveTexture = GL.GetInteger(GetPName.ActiveTexture);
        GL.ActiveTexture(TextureUnit.Texture0);
        int prevTexture2D = GL.GetInteger(GetPName.TextureBinding2D);
        Span<int> prevScissorBox = stackalloc int[4];
        unsafe
        {
            fixed (int* iptr = &prevScissorBox[0])
            {
                GL.GetInteger(GetPName.ScissorBox, iptr);
            }
        }
        Span<int> prevPolygonMode = stackalloc int[2];
        unsafe
        {
            fixed (int* iptr = &prevPolygonMode[0])
            {
                GL.GetInteger(GetPName.PolygonMode, iptr);
            }
        }

        if (_glVersion <= 310 || _compatibilityProfile)
        {
            GL.PolygonMode(TriangleFace.Front, PolygonMode.Fill);
            GL.PolygonMode(TriangleFace.Back, PolygonMode.Fill);
        }
        else
        {
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Fill);
        }

        // Bind the element buffer (thru the VAO) so that we can resize it.
        GL.BindVertexArray(_vertexArray);
        // Bind the vertex buffer so that we can resize it.
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
        for (int i = 0; i < drawData.CmdListsCount; i++)
        {
            ImDrawListPtr cmdList = drawData.CmdLists[i];

            int vertexSize = cmdList.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>();
            if (vertexSize > _vertexBufferSize)
            {
                int newSize = (int)Math.Max(_vertexBufferSize * 1.5f, vertexSize);

                GL.BufferData(BufferTarget.ArrayBuffer, newSize, IntPtr.Zero, BufferUsageHint.DynamicDraw);
                _vertexBufferSize = newSize;

                Console.WriteLine($"Resized dear imgui vertex buffer to new size {_vertexBufferSize}");
            }

            int indexSize = cmdList.IdxBuffer.Size * sizeof(ushort);
            if (indexSize > _indexBufferSize)
            {
                int newSize = (int)Math.Max(_indexBufferSize * 1.5f, indexSize);
                GL.BufferData(BufferTarget.ElementArrayBuffer, newSize, IntPtr.Zero, BufferUsageHint.DynamicDraw);
                _indexBufferSize = newSize;

                Console.WriteLine($"Resized dear imgui index buffer to new size {_indexBufferSize}");
            }
        }

        // Setup orthographic projection matrix into our constant buffer
        ImGuiIOPtr io = ImGui.GetIO();
        OpenTK.Mathematics.Matrix4 mvp = OpenTK.Mathematics.Matrix4.CreateOrthographicOffCenter(
            0.0f,
            io.DisplaySize.X,
            io.DisplaySize.Y,
            0.0f,
            -1.0f,
            1.0f);

        GL.UseProgram(_shader);
        GL.UniformMatrix4(_projectionMatrixUniformLocation, false, ref mvp);
        GL.Uniform1(_fontTextureUniformLocation, 0);
        CheckGLError("Projection");

        GL.BindVertexArray(_vertexArray);
        CheckGLError("VAO");

        drawData.ScaleClipRects(io.DisplayFramebufferScale);

        GL.Enable(EnableCap.Blend);
        GL.Enable(EnableCap.ScissorTest);
        GL.BlendEquation(BlendEquationMode.FuncAdd);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.Disable(EnableCap.CullFace);
        GL.Disable(EnableCap.DepthTest);

        // Render command lists
        for (int n = 0; n < drawData.CmdListsCount; n++)
        {
            ImDrawListPtr cmd_list = drawData.CmdLists[n];

            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, cmd_list.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>(), cmd_list.VtxBuffer.Data);
            CheckGLError($"Data Vert {n}");

            GL.BufferSubData(BufferTarget.ElementArrayBuffer, IntPtr.Zero, cmd_list.IdxBuffer.Size * sizeof(ushort), cmd_list.IdxBuffer.Data);
            CheckGLError($"Data Idx {n}");

            for (int cmd_i = 0; cmd_i < cmd_list.CmdBuffer.Size; cmd_i++)
            {
                ImDrawCmdPtr pcmd = cmd_list.CmdBuffer[cmd_i];
                if (pcmd.UserCallback != IntPtr.Zero)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, (int)pcmd.TextureId);
                    CheckGLError("Texture");

                    // We do _windowHeight - (int)clip.W instead of (int)clip.Y because gl has flipped Y when it comes to these coordinates
                    var clip = pcmd.ClipRect;
                    GL.Scissor((int)clip.X, _game!.ClientSize.Y - (int)clip.W, (int)(clip.Z - clip.X), (int)(clip.W - clip.Y));
                    CheckGLError("Scissor");

                    if ((io.BackendFlags & ImGuiBackendFlags.RendererHasVtxOffset) != 0)
                    {
                        GL.DrawElementsBaseVertex(PrimitiveType.Triangles, (int)pcmd.ElemCount, DrawElementsType.UnsignedShort, (IntPtr)(pcmd.IdxOffset * sizeof(ushort)), unchecked((int)pcmd.VtxOffset));
                    }
                    else
                    {
                        GL.DrawElements(BeginMode.Triangles, (int)pcmd.ElemCount, DrawElementsType.UnsignedShort, (int)pcmd.IdxOffset * sizeof(ushort));
                    }
                    CheckGLError("Draw");
                }
            }
        }

        GL.Disable(EnableCap.Blend);
        GL.Disable(EnableCap.ScissorTest);

        // Reset state
        GL.BindTexture(TextureTarget.Texture2D, prevTexture2D);
        GL.ActiveTexture((TextureUnit)prevActiveTexture);
        GL.UseProgram(prevProgram);
        GL.BindVertexArray(prevVAO);
        GL.Scissor(prevScissorBox[0], prevScissorBox[1], prevScissorBox[2], prevScissorBox[3]);
        GL.BindBuffer(BufferTarget.ArrayBuffer, prevArrayBuffer);
        GL.BlendEquationSeparate((BlendEquationMode)prevBlendEquationRgb, (BlendEquationMode)prevBlendEquationAlpha);
        GL.BlendFuncSeparate(
            (BlendingFactorSrc)prevBlendFuncSrcRgb,
            (BlendingFactorDest)prevBlendFuncDstRgb,
            (BlendingFactorSrc)prevBlendFuncSrcAlpha,
            (BlendingFactorDest)prevBlendFuncDstAlpha);
        if (prevBlendEnabled) GL.Enable(EnableCap.Blend); else GL.Disable(EnableCap.Blend);
        if (prevDepthTestEnabled) GL.Enable(EnableCap.DepthTest); else GL.Disable(EnableCap.DepthTest);
        if (prevCullFaceEnabled) GL.Enable(EnableCap.CullFace); else GL.Disable(EnableCap.CullFace);
        if (prevScissorTestEnabled) GL.Enable(EnableCap.ScissorTest); else GL.Disable(EnableCap.ScissorTest);
        if (_glVersion <= 310 || _compatibilityProfile)
        {
            GL.PolygonMode(TriangleFace.Front, (PolygonMode)prevPolygonMode[0]);
            GL.PolygonMode(TriangleFace.Back, (PolygonMode)prevPolygonMode[1]);
        }
        else
        {
            GL.PolygonMode(TriangleFace.FrontAndBack, (PolygonMode)prevPolygonMode[0]);
        }
    }

    public static ImGuiKey TranslateKey(Keys key) => key switch
    {
        Keys.Tab => ImGuiKey.Tab,
        Keys.Left => ImGuiKey.LeftArrow,
        Keys.Right => ImGuiKey.RightArrow,
        Keys.Up => ImGuiKey.UpArrow,
        Keys.Down => ImGuiKey.DownArrow,
        Keys.PageUp => ImGuiKey.PageUp,
        Keys.PageDown => ImGuiKey.PageDown,
        Keys.Home => ImGuiKey.Home,
        Keys.End => ImGuiKey.End,
        Keys.Insert => ImGuiKey.Insert,
        Keys.Delete => ImGuiKey.Delete,
        Keys.Backspace => ImGuiKey.Backspace,
        Keys.Space => ImGuiKey.Space,
        Keys.Enter => ImGuiKey.Enter,
        Keys.Escape => ImGuiKey.Escape,
        Keys.Apostrophe => ImGuiKey.Apostrophe,
        Keys.Comma => ImGuiKey.Comma,
        Keys.Minus => ImGuiKey.Minus,
        Keys.Period => ImGuiKey.Period,
        Keys.Slash => ImGuiKey.Slash,
        Keys.Semicolon => ImGuiKey.Semicolon,
        Keys.Equal => ImGuiKey.Equal,
        Keys.LeftBracket => ImGuiKey.LeftBracket,
        Keys.Backslash => ImGuiKey.Backslash,
        Keys.RightBracket => ImGuiKey.RightBracket,
        Keys.GraveAccent => ImGuiKey.GraveAccent,
        Keys.CapsLock => ImGuiKey.CapsLock,
        Keys.ScrollLock => ImGuiKey.ScrollLock,
        Keys.NumLock => ImGuiKey.NumLock,
        Keys.PrintScreen => ImGuiKey.PrintScreen,
        Keys.Pause => ImGuiKey.Pause,
        Keys.KeyPad0 => ImGuiKey.Keypad0,
        Keys.KeyPad1 => ImGuiKey.Keypad1,
        Keys.KeyPad2 => ImGuiKey.Keypad2,
        Keys.KeyPad3 => ImGuiKey.Keypad3,
        Keys.KeyPad4 => ImGuiKey.Keypad4,
        Keys.KeyPad5 => ImGuiKey.Keypad5,
        Keys.KeyPad6 => ImGuiKey.Keypad6,
        Keys.KeyPad7 => ImGuiKey.Keypad7,
        Keys.KeyPad8 => ImGuiKey.Keypad8,
        Keys.KeyPad9 => ImGuiKey.Keypad9,
        Keys.KeyPadDecimal => ImGuiKey.KeypadDecimal,
        Keys.KeyPadDivide => ImGuiKey.KeypadDivide,
        Keys.KeyPadMultiply => ImGuiKey.KeypadMultiply,
        Keys.KeyPadSubtract => ImGuiKey.KeypadSubtract,
        Keys.KeyPadAdd => ImGuiKey.KeypadAdd,
        Keys.KeyPadEnter => ImGuiKey.KeypadEnter,
        Keys.KeyPadEqual => ImGuiKey.KeypadEqual,
        Keys.LeftShift => ImGuiKey.LeftShift,
        Keys.LeftControl => ImGuiKey.LeftCtrl,
        Keys.LeftAlt => ImGuiKey.LeftAlt,
        Keys.LeftSuper => ImGuiKey.LeftSuper,
        Keys.RightShift => ImGuiKey.RightShift,
        Keys.RightControl => ImGuiKey.RightCtrl,
        Keys.RightAlt => ImGuiKey.RightAlt,
        Keys.RightSuper => ImGuiKey.RightSuper,
        Keys.Menu => ImGuiKey.Menu,
        Keys.D0 => ImGuiKey._0,
        Keys.D1 => ImGuiKey._1,
        Keys.D2 => ImGuiKey._2,
        Keys.D3 => ImGuiKey._3,
        Keys.D4 => ImGuiKey._4,
        Keys.D5 => ImGuiKey._5,
        Keys.D6 => ImGuiKey._6,
        Keys.D7 => ImGuiKey._7,
        Keys.D8 => ImGuiKey._8,
        Keys.D9 => ImGuiKey._9,
        Keys.A => ImGuiKey.A,
        Keys.B => ImGuiKey.B,
        Keys.C => ImGuiKey.C,
        Keys.D => ImGuiKey.D,
        Keys.E => ImGuiKey.E,
        Keys.F => ImGuiKey.F,
        Keys.G => ImGuiKey.G,
        Keys.H => ImGuiKey.H,
        Keys.I => ImGuiKey.I,
        Keys.J => ImGuiKey.J,
        Keys.K => ImGuiKey.K,
        Keys.L => ImGuiKey.L,
        Keys.M => ImGuiKey.M,
        Keys.N => ImGuiKey.N,
        Keys.O => ImGuiKey.O,
        Keys.P => ImGuiKey.P,
        Keys.Q => ImGuiKey.Q,
        Keys.R => ImGuiKey.R,
        Keys.S => ImGuiKey.S,
        Keys.T => ImGuiKey.T,
        Keys.U => ImGuiKey.U,
        Keys.V => ImGuiKey.V,
        Keys.W => ImGuiKey.W,
        Keys.X => ImGuiKey.X,
        Keys.Y => ImGuiKey.Y,
        Keys.Z => ImGuiKey.Z,
        Keys.F1 => ImGuiKey.F1,
        Keys.F2 => ImGuiKey.F2,
        Keys.F3 => ImGuiKey.F3,
        Keys.F4 => ImGuiKey.F4,
        Keys.F5 => ImGuiKey.F5,
        Keys.F6 => ImGuiKey.F6,
        Keys.F7 => ImGuiKey.F7,
        Keys.F8 => ImGuiKey.F8,
        Keys.F9 => ImGuiKey.F9,
        Keys.F10 => ImGuiKey.F10,
        Keys.F11 => ImGuiKey.F11,
        Keys.F12 => ImGuiKey.F12,
        Keys.F13 => ImGuiKey.F13,
        Keys.F14 => ImGuiKey.F14,
        Keys.F15 => ImGuiKey.F15,
        Keys.F16 => ImGuiKey.F16,
        Keys.F17 => ImGuiKey.F17,
        Keys.F18 => ImGuiKey.F18,
        Keys.F19 => ImGuiKey.F19,
        Keys.F20 => ImGuiKey.F20,
        Keys.F21 => ImGuiKey.F21,
        Keys.F22 => ImGuiKey.F22,
        Keys.F23 => ImGuiKey.F23,
        Keys.F24 => ImGuiKey.F24,
        _ => ImGuiKey.None
    };

    #endregion

    #region Initialization

    private void CreateDeviceResources()
    {
        _vertexBufferSize = 10000;
        _indexBufferSize = 2000;

        int prevVAO = GL.GetInteger(GetPName.VertexArrayBinding);
        int prevArrayBuffer = GL.GetInteger(GetPName.ArrayBufferBinding);

        _vertexArray = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArray);
        LabelObject(ObjectLabelIdentifier.VertexArray, _vertexArray, "ImGui");

        _vertexBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
        LabelObject(ObjectLabelIdentifier.Buffer, _vertexBuffer, "VBO: ImGui");
        GL.BufferData(BufferTarget.ArrayBuffer, _vertexBufferSize, IntPtr.Zero, BufferUsageHint.DynamicDraw);

        _indexBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBuffer);
        LabelObject(ObjectLabelIdentifier.Buffer, _indexBuffer, "EBO: ImGui");
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indexBufferSize, IntPtr.Zero, BufferUsageHint.DynamicDraw);

        RecreateFontDeviceTexture();

        string vertexSource = """
            #version 330 core

            uniform mat4 projection_matrix;

            layout(location = 0) in vec2 in_position;
            layout(location = 1) in vec2 in_texCoord;
            layout(location = 2) in vec4 in_color;

            out vec4 color;
            out vec2 texCoord;

            void main()
            {
                gl_Position = projection_matrix * vec4(in_position, 0, 1);
                color = in_color;
                texCoord = in_texCoord;
            }
            """;

        string fragmentSource = """
            #version 330 core

            uniform sampler2D in_fontTexture;

            in vec4 color;
            in vec2 texCoord;

            out vec4 outputColor;

            void main()
            {
                outputColor = color * texture(in_fontTexture, texCoord);
            }
            """;

        _shader = CreateProgram("ImGui", vertexSource, fragmentSource);
        _projectionMatrixUniformLocation = GL.GetUniformLocation(_shader, "projection_matrix");
        _fontTextureUniformLocation = GL.GetUniformLocation(_shader, "in_fontTexture");

        int stride = Unsafe.SizeOf<ImDrawVert>();
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, stride, 0);
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, stride, 8);
        GL.VertexAttribPointer(2, 4, VertexAttribPointerType.UnsignedByte, true, stride, 16);

        GL.EnableVertexAttribArray(0);
        GL.EnableVertexAttribArray(1);
        GL.EnableVertexAttribArray(2);

        GL.BindVertexArray(prevVAO);
        GL.BindBuffer(BufferTarget.ArrayBuffer, prevArrayBuffer);

        CheckGLError("End of ImGui setup");
    }

    private int CreateProgram(string name, string vertexSource, string fragmentSoruce)
    {
        int program = GL.CreateProgram();
        LabelObject(ObjectLabelIdentifier.Program, program, $"Program: {name}");

        int vertex = CompileShader(name, ShaderType.VertexShader, vertexSource);
        int fragment = CompileShader(name, ShaderType.FragmentShader, fragmentSoruce);

        GL.AttachShader(program, vertex);
        GL.AttachShader(program, fragment);

        GL.LinkProgram(program);

        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string info = GL.GetProgramInfoLog(program);
            Debug.WriteLine($"GL.LinkProgram had info log [{name}]:\n{info}");
        }

        GL.DetachShader(program, vertex);
        GL.DetachShader(program, fragment);

        GL.DeleteShader(vertex);
        GL.DeleteShader(fragment);

        return program;
    }

    private int CompileShader(string name, ShaderType type, string source)
    {
        int shader = GL.CreateShader(type);
        LabelObject(ObjectLabelIdentifier.Shader, shader, $"Shader: {name}");

        GL.ShaderSource(shader, source);
        GL.CompileShader(shader);

        GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
        if (success == 0)
        {
            string info = GL.GetShaderInfoLog(shader);
            Debug.WriteLine($"GL.CompileShader for shader '{name}' [{type}] had info log:\n{info}");
        }

        return shader;
    }

    /// <summary>
    /// Recreates the device texture used to render text.
    /// </summary>
    private void RecreateFontDeviceTexture()
    {
        ImGuiIOPtr io = ImGui.GetIO();
        io.Fonts.GetTexDataAsRGBA32(out IntPtr pixels, out int width, out int height, out int bytesPerPixel);

        int mips = (int)Math.Floor(Math.Log(Math.Max(width, height), 2));

        int prevActiveTexture = GL.GetInteger(GetPName.ActiveTexture);
        GL.ActiveTexture(TextureUnit.Texture0);
        int prevTexture2D = GL.GetInteger(GetPName.TextureBinding2D);

        _fontTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, _fontTexture);
        GL.TexStorage2D(TextureTarget2d.Texture2D, mips, SizedInternalFormat.Rgba8, width, height);
        LabelObject(ObjectLabelIdentifier.Texture, _fontTexture, "ImGui Text Atlas");

        GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, width, height, PixelFormat.Bgra, PixelType.UnsignedByte, pixels);

        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, mips - 1);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

        // Restore state
        GL.BindTexture(TextureTarget.Texture2D, prevTexture2D);
        GL.ActiveTexture((TextureUnit)prevActiveTexture);

        io.Fonts.SetTexID((IntPtr)_fontTexture);

        io.Fonts.ClearTexData();
    }

    private void CheckGLError(string title)
    {
        OpenTK.Graphics.OpenGL4.ErrorCode error;
        int i = 1;
        while ((error = GL.GetError()) != OpenTK.Graphics.OpenGL4.ErrorCode.NoError)
        {
            Debug.Print($"{title} ({i++}): {error}");
        }
    }

    private bool IsExtensionSupported(string name)
    {
        int n = GL.GetInteger(GetPName.NumExtensions);
        for (int i = 0; i < n; i++)
        {
            string extension = GL.GetString(StringNameIndexed.Extensions, i);
            if (extension == name) return true;
        }

        return false;
    }

    private void LabelObject(ObjectLabelIdentifier objLabelIdent, int glObject, string name)
    {
        if (_KHRDebugAvailable)
            GL.ObjectLabel(objLabelIdent, glObject, name.Length, name);
    }

    #endregion
}
