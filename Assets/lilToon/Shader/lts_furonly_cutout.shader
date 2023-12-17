Shader "_lil/[Optional] lilToonFurOnlyCutout"
{
    Properties
    {
        //----------------------------------------------------------------------------------------------------------------------
        // Base
             _Invisible                  ("", Int) = 0
                        _AsUnlit                    ("", Range(0, 1)) = 0
                        _Cutoff                     ("", Range(-0.001,1.001)) = 0.5
                        _SubpassCutoff              ("", Range(0,1)) = 0.5
             _FlipNormal                 ("", Int) = 0
             _ShiftBackfaceUV            ("", Int) = 0
                        _BackfaceForceShadow        ("", Range(0,1)) = 0
                _BackfaceColor              ("", Color) = (0,0,0,0)
                        _VertexLightStrength        ("", Range(0,1)) = 0
                        _LightMinLimit              ("", Range(0,1)) = 0.05
                        _LightMaxLimit              ("", Range(0,10)) = 1
                        _BeforeExposureLimit        ("", Float) = 10000
                        _MonochromeLighting         ("", Range(0,1)) = 0
                        _AlphaBoostFA               ("", Range(1,100)) = 10
                        _lilDirectionalLightStrength ("", Range(0,1)) = 1
              _LightDirectionOverride     ("", Vector) = (0.001,0.002,0.001,0)
                        _AAStrength                 ("", Range(0, 1)) = 1
             _UseDither                  ("", Int) = 0
         _DitherTex                  ("", 2D) = "white" {}
                        _DitherMaxValue             ("", Float) = 255

        //----------------------------------------------------------------------------------------------------------------------
        // Main
         [MainColor] _Color                 ("", Color) = (1,1,1,1)
        [MainTexture]   _MainTex                    ("", 2D) = "white" {}
             _MainTex_ScrollRotate       ("", Vector) = (0,0,0,0)
               _MainTexHSVG                ("", Vector) = (0,1,1,1)
                        _MainGradationStrength      ("", Range(0, 1)) = 0
         _MainGradationTex           ("", 2D) = "white" {}
         _MainColorAdjustMask        ("", 2D) = "white" {}

        //----------------------------------------------------------------------------------------------------------------------
        // Main2nd
         _UseMain2ndTex              ("", Int) = 0
                _Color2nd                   ("", Color) = (1,1,1,1)
                        _Main2ndTex                 ("", 2D) = "white" {}
              _Main2ndTexAngle            ("", Float) = 0
             _Main2ndTex_ScrollRotate    ("", Vector) = (0,0,0,0)
               _Main2ndTex_UVMode          ("", Int) = 0
               _Main2ndTex_Cull            ("", Int) = 0
          _Main2ndTexDecalAnimation   ("", Vector) = (1,1,1,30)
           _Main2ndTexDecalSubParam    ("", Vector) = (1,1,0,1)
             _Main2ndTexIsDecal          ("", Int) = 0
             _Main2ndTexIsLeftOnly       ("", Int) = 0
             _Main2ndTexIsRightOnly      ("", Int) = 0
             _Main2ndTexShouldCopy       ("", Int) = 0
             _Main2ndTexShouldFlipMirror ("", Int) = 0
             _Main2ndTexShouldFlipCopy   ("", Int) = 0
             _Main2ndTexIsMSDF           ("", Int) = 0
         _Main2ndBlendMask           ("", 2D) = "white" {}
               _Main2ndTexBlendMode        ("", Int) = 0
               _Main2ndTexAlphaMode        ("", Int) = 0
                        _Main2ndEnableLighting      ("", Range(0, 1)) = 1
                        _Main2ndDissolveMask        ("", 2D) = "white" {}
                        _Main2ndDissolveNoiseMask   ("", 2D) = "gray" {}
             _Main2ndDissolveNoiseMask_ScrollRotate ("", Vector) = (0,0,0,0)
                        _Main2ndDissolveNoiseStrength ("", float) = 0.1
                _Main2ndDissolveColor       ("", Color) = (1,1,1,1)
           _Main2ndDissolveParams      ("", Vector) = (0,0,0.5,0.1)
          _Main2ndDissolvePos         ("", Vector) = (0,0,0,0)
               _Main2ndDistanceFade        ("", Vector) = (0.1,0.01,0,0)

        //----------------------------------------------------------------------------------------------------------------------
        // Main3rd
         _UseMain3rdTex              ("", Int) = 0
                _Color3rd                   ("", Color) = (1,1,1,1)
                        _Main3rdTex                 ("", 2D) = "white" {}
              _Main3rdTexAngle            ("", Float) = 0
             _Main3rdTex_ScrollRotate    ("", Vector) = (0,0,0,0)
               _Main3rdTex_UVMode          ("", Int) = 0
               _Main3rdTex_Cull            ("", Int) = 0
          _Main3rdTexDecalAnimation   ("", Vector) = (1,1,1,30)
           _Main3rdTexDecalSubParam    ("", Vector) = (1,1,0,1)
             _Main3rdTexIsDecal          ("", Int) = 0
             _Main3rdTexIsLeftOnly       ("", Int) = 0
             _Main3rdTexIsRightOnly      ("", Int) = 0
             _Main3rdTexShouldCopy       ("", Int) = 0
             _Main3rdTexShouldFlipMirror ("", Int) = 0
             _Main3rdTexShouldFlipCopy   ("", Int) = 0
             _Main3rdTexIsMSDF           ("", Int) = 0
         _Main3rdBlendMask           ("", 2D) = "white" {}
               _Main3rdTexBlendMode        ("", Int) = 0
               _Main3rdTexAlphaMode        ("", Int) = 0
                        _Main3rdEnableLighting      ("", Range(0, 1)) = 1
                        _Main3rdDissolveMask        ("", 2D) = "white" {}
                        _Main3rdDissolveNoiseMask   ("", 2D) = "gray" {}
             _Main3rdDissolveNoiseMask_ScrollRotate ("", Vector) = (0,0,0,0)
                        _Main3rdDissolveNoiseStrength ("", float) = 0.1
                _Main3rdDissolveColor       ("", Color) = (1,1,1,1)
           _Main3rdDissolveParams      ("", Vector) = (0,0,0.5,0.1)
          _Main3rdDissolvePos         ("", Vector) = (0,0,0,0)
               _Main3rdDistanceFade        ("", Vector) = (0.1,0.01,0,0)

        //----------------------------------------------------------------------------------------------------------------------
        // Alpha Mask
          _AlphaMaskMode              ("", Int) = 0
                        _AlphaMask                  ("", 2D) = "white" {}
                        _AlphaMaskScale             ("", Float) = 1
                        _AlphaMaskValue             ("", Float) = 0

        //----------------------------------------------------------------------------------------------------------------------
        // NormalMap
         _UseBumpMap                 ("", Int) = 0
        [Normal]        _BumpMap                    ("", 2D) = "bump" {}
                        _BumpScale                  ("", Range(-10,10)) = 1

        //----------------------------------------------------------------------------------------------------------------------
        // NormalMap 2nd
         _UseBump2ndMap              ("", Int) = 0
        [Normal]        _Bump2ndMap                 ("", 2D) = "bump" {}
               _Bump2ndMap_UVMode          ("", Int) = 0
                        _Bump2ndScale               ("", Range(-10,10)) = 1
         _Bump2ndScaleMask           ("", 2D) = "white" {}

        //----------------------------------------------------------------------------------------------------------------------
        // Anisotropy
         _UseAnisotropy              ("", Int) = 0
        [Normal]        _AnisotropyTangentMap       ("", 2D) = "bump" {}
                        _AnisotropyScale            ("", Range(-1,1)) = 1
         _AnisotropyScaleMask        ("", 2D) = "white" {}
                        _AnisotropyTangentWidth     ("", Range(0,10)) = 1
                        _AnisotropyBitangentWidth   ("", Range(0,10)) = 1
                        _AnisotropyShift            ("", Range(-10,10)) = 0
                        _AnisotropyShiftNoiseScale  ("", Range(-1,1)) = 0
                        _AnisotropySpecularStrength ("", Range(0,10)) = 1
                        _Anisotropy2ndTangentWidth  ("", Range(0,10)) = 1
                        _Anisotropy2ndBitangentWidth ("", Range(0,10)) = 1
                        _Anisotropy2ndShift         ("", Range(-10,10)) = 0
                        _Anisotropy2ndShiftNoiseScale ("", Range(-1,1)) = 0
                        _Anisotropy2ndSpecularStrength ("", Range(0,10)) = 0
                        _AnisotropyShiftNoiseMask   ("", 2D) = "white" {}
             _Anisotropy2Reflection      ("", Int) = 0
             _Anisotropy2MatCap          ("", Int) = 0
             _Anisotropy2MatCap2nd       ("", Int) = 0

        //----------------------------------------------------------------------------------------------------------------------
        // Backlight
         _UseBacklight               ("", Int) = 0
                _BacklightColor             ("", Color) = (0.85,0.8,0.7,1.0)
         _BacklightColorTex          ("", 2D) = "white" {}
                        _BacklightMainStrength      ("", Range(0, 1)) = 0
                        _BacklightNormalStrength    ("", Range(0, 1)) = 1.0
                        _BacklightBorder            ("", Range(0, 1)) = 0.35
                        _BacklightBlur              ("", Range(0, 1)) = 0.05
                        _BacklightDirectivity       ("", Float) = 5.0
                        _BacklightViewStrength      ("", Range(0, 1)) = 1
             _BacklightReceiveShadow     ("", Int) = 1
             _BacklightBackfaceMask      ("", Int) = 1

        //----------------------------------------------------------------------------------------------------------------------
        // Shadow
         _UseShadow                  ("", Int) = 0
                        _ShadowStrength             ("", Range(0, 1)) = 1
         _ShadowStrengthMask         ("", 2D) = "white" {}
                _ShadowStrengthMaskLOD      ("", Range(0, 1)) = 0
         _ShadowBorderMask           ("", 2D) = "white" {}
                _ShadowBorderMaskLOD        ("", Range(0, 1)) = 0
         _ShadowBlurMask             ("", 2D) = "white" {}
                _ShadowBlurMaskLOD          ("", Range(0, 1)) = 0
               _ShadowAOShift              ("", Vector) = (1,0,1,0)
                 _ShadowAOShift2             ("", Vector) = (1,0,1,0)
             _ShadowPostAO               ("", Int) = 0
               _ShadowColorType            ("", Int) = 0
                        _ShadowColor                ("", Color) = (0.82,0.76,0.85,1.0)
         _ShadowColorTex             ("", 2D) = "black" {}
                        _ShadowNormalStrength       ("", Range(0, 1)) = 1.0
                        _ShadowBorder               ("", Range(0, 1)) = 0.5
                        _ShadowBlur                 ("", Range(0, 1)) = 0.1
                        _ShadowReceive              ("", Range(0, 1)) = 0
                        _Shadow2ndColor             ("", Color) = (0.68,0.66,0.79,1)
         _Shadow2ndColorTex          ("", 2D) = "black" {}
                        _Shadow2ndNormalStrength    ("", Range(0, 1)) = 1.0
                        _Shadow2ndBorder            ("", Range(0, 1)) = 0.15
                        _Shadow2ndBlur              ("", Range(0, 1)) = 0.1
                        _Shadow2ndReceive           ("", Range(0, 1)) = 0
                        _Shadow3rdColor             ("", Color) = (0,0,0,0)
         _Shadow3rdColorTex          ("", 2D) = "black" {}
                        _Shadow3rdNormalStrength    ("", Range(0, 1)) = 1.0
                        _Shadow3rdBorder            ("", Range(0, 1)) = 0.25
                        _Shadow3rdBlur              ("", Range(0, 1)) = 0.1
                        _Shadow3rdReceive           ("", Range(0, 1)) = 0
                        _ShadowBorderColor          ("", Color) = (1,0.1,0,1)
                        _ShadowBorderRange          ("", Range(0, 1)) = 0.08
                        _ShadowMainStrength         ("", Range(0, 1)) = 0
                        _ShadowEnvStrength          ("", Range(0, 1)) = 0
               _ShadowMaskType             ("", Int) = 0
                        _ShadowFlatBorder           ("", Range(-2, 2)) = 1
                        _ShadowFlatBlur             ("", Range(0.001, 2)) = 1

        //----------------------------------------------------------------------------------------------------------------------
        // Reflection
         _UseReflection              ("", Int) = 0
        // Smoothness
                        _Smoothness                 ("", Range(0, 1)) = 1
         _SmoothnessTex              ("", 2D) = "white" {}
        // Metallic
        [Gamma]         _Metallic                   ("", Range(0, 1)) = 0
         _MetallicGlossMap           ("", 2D) = "white" {}
        // Reflectance
        [Gamma]         _Reflectance                ("", Range(0, 1)) = 0.04
        // Reflection
                        _GSAAStrength               ("", Range(0, 1)) = 0
             _ApplySpecular              ("", Int) = 1
             _ApplySpecularFA            ("", Int) = 1
             _SpecularToon               ("", Int) = 1
                        _SpecularNormalStrength     ("", Range(0, 1)) = 1.0
                        _SpecularBorder             ("", Range(0, 1)) = 0.5
                        _SpecularBlur               ("", Range(0, 1)) = 0.0
             _ApplyReflection            ("", Int) = 0
                        _ReflectionNormalStrength   ("", Range(0, 1)) = 1.0
                _ReflectionColor            ("", Color) = (1,1,1,1)
         _ReflectionColorTex         ("", 2D) = "white" {}
             _ReflectionApplyTransparency ("", Int) = 1
         _ReflectionCubeTex          ("", Cube) = "black" {}
                _ReflectionCubeColor        ("", Color) = (0,0,0,1)
             _ReflectionCubeOverride     ("", Int) = 0
                        _ReflectionCubeEnableLighting ("", Range(0, 1)) = 1
               _ReflectionBlendMode        ("", Int) = 1

        //----------------------------------------------------------------------------------------------------------------------
        // MatCap
         _UseMatCap                  ("", Int) = 0
                _MatCapColor                ("", Color) = (1,1,1,1)
                        _MatCapTex                  ("", 2D) = "white" {}
                        _MatCapMainStrength         ("", Range(0, 1)) = 0
              _MatCapBlendUV1             ("", Vector) = (0,0,0,0)
             _MatCapZRotCancel           ("", Int) = 1
             _MatCapPerspective          ("", Int) = 1
                        _MatCapVRParallaxStrength   ("", Range(0, 1)) = 1
                        _MatCapBlend                ("", Range(0, 1)) = 1
         _MatCapBlendMask            ("", 2D) = "white" {}
                        _MatCapEnableLighting       ("", Range(0, 1)) = 1
                        _MatCapShadowMask           ("", Range(0, 1)) = 0
             _MatCapBackfaceMask         ("", Int) = 0
                        _MatCapLod                  ("", Range(0, 10)) = 0
               _MatCapBlendMode            ("", Int) = 1
             _MatCapApplyTransparency    ("", Int) = 1
                        _MatCapNormalStrength       ("", Range(0, 1)) = 1.0
             _MatCapCustomNormal         ("", Int) = 0
        [Normal]        _MatCapBumpMap              ("", 2D) = "bump" {}
                        _MatCapBumpScale            ("", Range(-10,10)) = 1

        //----------------------------------------------------------------------------------------------------------------------
        // MatCap 2nd
         _UseMatCap2nd               ("", Int) = 0
                _MatCap2ndColor             ("", Color) = (1,1,1,1)
                        _MatCap2ndTex               ("", 2D) = "white" {}
                        _MatCap2ndMainStrength      ("", Range(0, 1)) = 0
              _MatCap2ndBlendUV1          ("", Vector) = (0,0,0,0)
             _MatCap2ndZRotCancel        ("", Int) = 1
             _MatCap2ndPerspective       ("", Int) = 1
                        _MatCap2ndVRParallaxStrength ("", Range(0, 1)) = 1
                        _MatCap2ndBlend             ("", Range(0, 1)) = 1
         _MatCap2ndBlendMask         ("", 2D) = "white" {}
                        _MatCap2ndEnableLighting    ("", Range(0, 1)) = 1
                        _MatCap2ndShadowMask        ("", Range(0, 1)) = 0
             _MatCap2ndBackfaceMask      ("", Int) = 0
                        _MatCap2ndLod               ("", Range(0, 10)) = 0
               _MatCap2ndBlendMode         ("", Int) = 1
             _MatCap2ndApplyTransparency ("", Int) = 1
                        _MatCap2ndNormalStrength    ("", Range(0, 1)) = 1.0
             _MatCap2ndCustomNormal      ("", Int) = 0
        [Normal]        _MatCap2ndBumpMap           ("", 2D) = "bump" {}
                        _MatCap2ndBumpScale         ("", Range(-10,10)) = 1

        //----------------------------------------------------------------------------------------------------------------------
        // Rim
         _UseRim                     ("", Int) = 0
                _RimColor                   ("", Color) = (0.66,0.5,0.48,1)
         _RimColorTex                ("", 2D) = "white" {}
                        _RimMainStrength            ("", Range(0, 1)) = 0
                        _RimNormalStrength          ("", Range(0, 1)) = 1.0
                        _RimBorder                  ("", Range(0, 1)) = 0.5
                        _RimBlur                    ("", Range(0, 1)) = 0.65
        _RimFresnelPower          ("", Range(0.01, 50)) = 3.5
                        _RimEnableLighting          ("", Range(0, 1)) = 1
                        _RimShadowMask              ("", Range(0, 1)) = 0.5
             _RimBackfaceMask            ("", Int) = 1
                        _RimVRParallaxStrength      ("", Range(0, 1)) = 1
             _RimApplyTransparency       ("", Int) = 1
                        _RimDirStrength             ("", Range(0, 1)) = 0
                        _RimDirRange                ("", Range(-1, 1)) = 0
                        _RimIndirRange              ("", Range(-1, 1)) = 0
                _RimIndirColor              ("", Color) = (1,1,1,1)
                        _RimIndirBorder             ("", Range(0, 1)) = 0.5
                        _RimIndirBlur               ("", Range(0, 1)) = 0.1
               _RimBlendMode               ("", Int) = 1

        //----------------------------------------------------------------------------------------------------------------------
        // Glitter
         _UseGlitter                 ("", Int) = 0
               _GlitterUVMode              ("", Int) = 0
                _GlitterColor               ("", Color) = (1,1,1,1)
                        _GlitterColorTex            ("", 2D) = "white" {}
               _GlitterColorTex_UVMode     ("", Int) = 0
                        _GlitterMainStrength        ("", Range(0, 1)) = 0
                        _GlitterNormalStrength      ("", Range(0, 1)) = 1.0
                        _GlitterScaleRandomize      ("", Range(0, 1)) = 0
             _GlitterApplyShape          ("", Int) = 0
                        _GlitterShapeTex            ("", 2D) = "white" {}
               _GlitterAtras               ("", Vector) = (1,1,0,0)
             _GlitterAngleRandomize      ("", Int) = 0
         _GlitterParams1             ("", Vector) = (256,256,0.16,50)
         _GlitterParams2             ("", Vector) = (0.25,0,0,0)
                        _GlitterPostContrast        ("", Float) = 1
                        _GlitterSensitivity         ("", Float) = 0.25
                        _GlitterEnableLighting      ("", Range(0, 1)) = 1
                        _GlitterShadowMask          ("", Range(0, 1)) = 0
             _GlitterBackfaceMask        ("", Int) = 0
             _GlitterApplyTransparency   ("", Int) = 1
                        _GlitterVRParallaxStrength  ("", Range(0, 1)) = 0

        //----------------------------------------------------------------------------------------------------------------------
        // Emmision
         _UseEmission                ("", Int) = 0
        [HDR]   _EmissionColor              ("", Color) = (1,1,1,1)
                        _EmissionMap                ("", 2D) = "white" {}
             _EmissionMap_ScrollRotate   ("", Vector) = (0,0,0,0)
               _EmissionMap_UVMode         ("", Int) = 0
                        _EmissionMainStrength       ("", Range(0, 1)) = 0
                        _EmissionBlend              ("", Range(0,1)) = 1
                        _EmissionBlendMask          ("", 2D) = "white" {}
             _EmissionBlendMask_ScrollRotate ("", Vector) = (0,0,0,0)
               _EmissionBlendMode          ("", Int) = 1
              _EmissionBlink              ("", Vector) = (0,0,3.141593,0)
             _EmissionUseGrad            ("", Int) = 0
         _EmissionGradTex            ("", 2D) = "white" {}
                        _EmissionGradSpeed          ("", Float) = 1
                        _EmissionParallaxDepth      ("", float) = 0
                        _EmissionFluorescence       ("", Range(0,1)) = 0
        // Gradation
         _egci ("", Int) = 2
         _egai ("", Int) = 2
         _egc0 ("", Color) = (1,1,1,0)
         _egc1 ("", Color) = (1,1,1,1)
         _egc2 ("", Color) = (1,1,1,0)
         _egc3 ("", Color) = (1,1,1,0)
         _egc4 ("", Color) = (1,1,1,0)
         _egc5 ("", Color) = (1,1,1,0)
         _egc6 ("", Color) = (1,1,1,0)
         _egc7 ("", Color) = (1,1,1,0)
         _ega0 ("", Color) = (1,0,0,0)
         _ega1 ("", Color) = (1,0,0,1)
         _ega2 ("", Color) = (1,0,0,0)
         _ega3 ("", Color) = (1,0,0,0)
         _ega4 ("", Color) = (1,0,0,0)
         _ega5 ("", Color) = (1,0,0,0)
         _ega6 ("", Color) = (1,0,0,0)
         _ega7 ("", Color) = (1,0,0,0)

        //----------------------------------------------------------------------------------------------------------------------
        // Emmision2nd
         _UseEmission2nd             ("", Int) = 0
        [HDR]   _Emission2ndColor           ("", Color) = (1,1,1,1)
                        _Emission2ndMap             ("", 2D) = "white" {}
             _Emission2ndMap_ScrollRotate ("", Vector) = (0,0,0,0)
               _Emission2ndMap_UVMode      ("", Int) = 0
                        _Emission2ndMainStrength    ("", Range(0, 1)) = 0
                        _Emission2ndBlend           ("", Range(0,1)) = 1
                        _Emission2ndBlendMask       ("", 2D) = "white" {}
             _Emission2ndBlendMask_ScrollRotate ("", Vector) = (0,0,0,0)
               _Emission2ndBlendMode       ("", Int) = 1
              _Emission2ndBlink           ("", Vector) = (0,0,3.141593,0)
             _Emission2ndUseGrad         ("", Int) = 0
         _Emission2ndGradTex         ("", 2D) = "white" {}
                        _Emission2ndGradSpeed       ("", Float) = 1
                        _Emission2ndParallaxDepth   ("", float) = 0
                        _Emission2ndFluorescence    ("", Range(0,1)) = 0
        // Gradation
         _e2gci ("", Int) = 2
         _e2gai ("", Int) = 2
         _e2gc0 ("", Color) = (1,1,1,0)
         _e2gc1 ("", Color) = (1,1,1,1)
         _e2gc2 ("", Color) = (1,1,1,0)
         _e2gc3 ("", Color) = (1,1,1,0)
         _e2gc4 ("", Color) = (1,1,1,0)
         _e2gc5 ("", Color) = (1,1,1,0)
         _e2gc6 ("", Color) = (1,1,1,0)
         _e2gc7 ("", Color) = (1,1,1,0)
         _e2ga0 ("", Color) = (1,0,0,0)
         _e2ga1 ("", Color) = (1,0,0,1)
         _e2ga2 ("", Color) = (1,0,0,0)
         _e2ga3 ("", Color) = (1,0,0,0)
         _e2ga4 ("", Color) = (1,0,0,0)
         _e2ga5 ("", Color) = (1,0,0,0)
         _e2ga6 ("", Color) = (1,0,0,0)
         _e2ga7 ("", Color) = (1,0,0,0)

        //----------------------------------------------------------------------------------------------------------------------
        // Parallax
         _UseParallax                ("", Int) = 0
             _UsePOM                     ("", Int) = 0
         _ParallaxMap                ("", 2D) = "gray" {}
                        _Parallax                   ("", float) = 0.02
                        _ParallaxOffset             ("", float) = 0.5

        //----------------------------------------------------------------------------------------------------------------------
        // Distance Fade
                _DistanceFadeColor          ("", Color) = (0,0,0,1)
               _DistanceFade               ("", Vector) = (0.1,0.01,0,0)
               _DistanceFadeMode           ("", Int) = 0
                _DistanceFadeRimColor       ("", Color) = (0,0,0,0)
        _DistanceFadeRimFresnelPower ("", Range(0.01, 50)) = 5.0

        //----------------------------------------------------------------------------------------------------------------------
        // AudioLink
         _UseAudioLink               ("", Int) = 0
               _AudioLinkDefaultValue      ("", Vector) = (0.0,0.0,2.0,0.75)
               _AudioLinkUVMode            ("", Int) = 1
         _AudioLinkUVParams          ("", Vector) = (0.25,0,0,0.125)
               _AudioLinkStart             ("", Vector) = (0.0,0.0,0.0,0.0)
                        _AudioLinkMask              ("", 2D) = "blue" {}
             _AudioLinkMask_ScrollRotate ("", Vector) = (0,0,0,0)
               _AudioLinkMask_UVMode       ("", Int) = 0
             _AudioLink2Main2nd          ("", Int) = 0
             _AudioLink2Main3rd          ("", Int) = 0
             _AudioLink2Emission         ("", Int) = 0
             _AudioLink2EmissionGrad     ("", Int) = 0
             _AudioLink2Emission2nd      ("", Int) = 0
             _AudioLink2Emission2ndGrad  ("", Int) = 0
             _AudioLink2Vertex           ("", Int) = 0
               _AudioLinkVertexUVMode      ("", Int) = 1
         _AudioLinkVertexUVParams    ("", Vector) = (0.25,0,0,0.125)
               _AudioLinkVertexStart       ("", Vector) = (0.0,0.0,0.0,0.0)
          _AudioLinkVertexStrength    ("", Vector) = (0.0,0.0,0.0,1.0)
             _AudioLinkAsLocal           ("", Int) = 0
         _AudioLinkLocalMap          ("", 2D) = "black" {}
            _AudioLinkLocalMapParams    ("", Vector) = (120,1,0,0)

        //----------------------------------------------------------------------------------------------------------------------
        // Dissolve
                        _DissolveMask               ("", 2D) = "white" {}
                        _DissolveNoiseMask          ("", 2D) = "gray" {}
             _DissolveNoiseMask_ScrollRotate ("", Vector) = (0,0,0,0)
                        _DissolveNoiseStrength      ("", float) = 0.1
                _DissolveColor              ("", Color) = (1,1,1,1)
           _DissolveParams             ("", Vector) = (0,0,0.5,0.1)
          _DissolvePos                ("", Vector) = (0,0,0,0)

        //----------------------------------------------------------------------------------------------------------------------
        // ID Mask
               _IDMaskFrom                 ("", Int) = 8
        [ToggleUI]      _IDMask1                    ("", Int) = 0
        [ToggleUI]      _IDMask2                    ("", Int) = 0
        [ToggleUI]      _IDMask3                    ("", Int) = 0
        [ToggleUI]      _IDMask4                    ("", Int) = 0
        [ToggleUI]      _IDMask5                    ("", Int) = 0
        [ToggleUI]      _IDMask6                    ("", Int) = 0
        [ToggleUI]      _IDMask7                    ("", Int) = 0
        [ToggleUI]      _IDMask8                    ("", Int) = 0
                        _IDMaskIndex1               ("", Int) = 0
                        _IDMaskIndex2               ("", Int) = 0
                        _IDMaskIndex3               ("", Int) = 0
                        _IDMaskIndex4               ("", Int) = 0
                        _IDMaskIndex5               ("", Int) = 0
                        _IDMaskIndex6               ("", Int) = 0
                        _IDMaskIndex7               ("", Int) = 0
                        _IDMaskIndex8               ("", Int) = 0

        //----------------------------------------------------------------------------------------------------------------------
        // Encryption
             _IgnoreEncryption           ("", Int) = 0
                        _Keys                       ("", Vector) = (0,0,0,0)
                        _BitKey0                    ("", Float) = 0
                        _BitKey1                    ("", Float) = 0
                        _BitKey2                    ("", Float) = 0
                        _BitKey3                    ("", Float) = 0
                        _BitKey4                    ("", Float) = 0
                        _BitKey5                    ("", Float) = 0
                        _BitKey6                    ("", Float) = 0
                        _BitKey7                    ("", Float) = 0
                        _BitKey8                    ("", Float) = 0
                        _BitKey9                    ("", Float) = 0
                        _BitKey10                   ("", Float) = 0
                        _BitKey11                   ("", Float) = 0
                        _BitKey12                   ("", Float) = 0
                        _BitKey13                   ("", Float) = 0
                        _BitKey14                   ("", Float) = 0
                        _BitKey15                   ("", Float) = 0
                        _BitKey16                   ("", Float) = 0
                        _BitKey17                   ("", Float) = 0
                        _BitKey18                   ("", Float) = 0
                        _BitKey19                   ("", Float) = 0
                        _BitKey20                   ("", Float) = 0
                        _BitKey21                   ("", Float) = 0
                        _BitKey22                   ("", Float) = 0
                        _BitKey23                   ("", Float) = 0
                        _BitKey24                   ("", Float) = 0
                        _BitKey25                   ("", Float) = 0
                        _BitKey26                   ("", Float) = 0
                        _BitKey27                   ("", Float) = 0
                        _BitKey28                   ("", Float) = 0
                        _BitKey29                   ("", Float) = 0
                        _BitKey30                   ("", Float) = 0
                        _BitKey31                   ("", Float) = 0

        //----------------------------------------------------------------------------------------------------------------------
        // Outline
                _OutlineColor               ("", Color) = (0.6,0.56,0.73,1)
                        _OutlineTex                 ("", 2D) = "white" {}
             _OutlineTex_ScrollRotate    ("", Vector) = (0,0,0,0)
               _OutlineTexHSVG             ("", Vector) = (0,1,1,1)
                _OutlineLitColor            ("", Color) = (1.0,0.2,0,0)
             _OutlineLitApplyTex         ("", Int) = 0
                        _OutlineLitScale            ("", Float) = 10
                        _OutlineLitOffset           ("", Float) = -8
             _OutlineLitShadowReceive    ("", Int) = 0
            _OutlineWidth               ("", Range(0,1)) = 0.08
         _OutlineWidthMask           ("", 2D) = "white" {}
                        _OutlineFixWidth            ("", Range(0,1)) = 0.5
               _OutlineVertexR2Width       ("", Int) = 0
             _OutlineDeleteMesh          ("", Int) = 0
        [Normal] _OutlineVectorTex   ("", 2D) = "bump" {}
               _OutlineVectorUVMode        ("", Int) = 0
                        _OutlineVectorScale         ("", Range(-10,10)) = 1
                        _OutlineEnableLighting      ("", Range(0, 1)) = 1
                        _OutlineZBias               ("", Float) = 0
             _OutlineDisableInVR         ("", Int) = 0

        //----------------------------------------------------------------------------------------------------------------------
        // Tessellation
                        _TessEdge                   ("", Range(1, 100)) = 10
                        _TessStrength               ("", Range(0, 1)) = 0.5
                        _TessShrink                 ("", Range(0, 1)) = 0.0
              _TessFactorMax              ("", Range(1, 8)) = 3

        //----------------------------------------------------------------------------------------------------------------------
        // For Multi
         _UseOutline                 ("", Int) = 0
               _TransparentMode            ("", Int) = 0
             _UseClippingCanceller       ("", Int) = 0
             _AsOverlay                  ("", Int) = 0

        //----------------------------------------------------------------------------------------------------------------------
        // Save (Unused)
                                       _BaseColor          ("", Color) = (1,1,1,1)
                                       _BaseMap            ("", 2D) = "white" {}
                                       _BaseColorMap       ("", 2D) = "white" {}
                                       _lilToonVersion     ("", Int) = 0

        //----------------------------------------------------------------------------------------------------------------------
        // Advanced
                                               _Cull               ("", Int) = 2
                 _SrcBlend           ("", Int) = 1
                 _DstBlend           ("", Int) = 0
                 _SrcBlendAlpha      ("", Int) = 1
                 _DstBlendAlpha      ("", Int) = 10
                   _BlendOp            ("", Int) = 0
                   _BlendOpAlpha       ("", Int) = 0
                 _SrcBlendFA         ("", Int) = 1
                 _DstBlendFA         ("", Int) = 1
                 _SrcBlendAlphaFA    ("", Int) = 0
                 _DstBlendAlphaFA    ("", Int) = 1
                   _BlendOpFA          ("", Int) = 4
                   _BlendOpAlphaFA     ("", Int) = 4
                                             _ZClip              ("", Int) = 1
                                             _ZWrite             ("", Int) = 1
           _ZTest              ("", Int) = 4
                                              _StencilRef         ("", Range(0, 255)) = 0
                                              _StencilReadMask    ("", Range(0, 255)) = 255
                                              _StencilWriteMask   ("", Range(0, 255)) = 255
           _StencilComp        ("", Float) = 8
                 _StencilPass        ("", Float) = 0
                 _StencilFail        ("", Float) = 0
                 _StencilZFail       ("", Float) = 0
                                                        _OffsetFactor       ("", Float) = 0
                                                        _OffsetUnits        ("", Float) = 0
                                          _ColorMask          ("", Int) = 15
                                             _AlphaToMask        ("", Int) = 1
                                                        _lilShadowCasterBias ("", Float) = 0

        //----------------------------------------------------------------------------------------------------------------------
        // Fur
                        _FurNoiseMask               ("", 2D) = "white" {}
         _FurMask                    ("", 2D) = "white" {}
         _FurLengthMask              ("", 2D) = "white" {}
        [Normal] _FurVectorTex       ("", 2D) = "bump" {}
                        _FurVectorScale             ("", Range(-10,10)) = 1
          _FurVector                  ("", Vector) = (0.0,0.0,1.0,0.02)
             _VertexColor2FurVector      ("", Int) = 0
                        _FurGravity                 ("", Range(0,1)) = 0.25
                        _FurRandomize               ("", Float) = 0
                        _FurAO                      ("", Range(0,1)) = 0
               _FurMeshType                ("", Int) = 1
              _FurLayerNum                ("", Range(1, 6)) = 2
                        _FurRootOffset              ("", Range(-1,0)) = 0
                        _FurCutoutLength            ("", Float) = 0.8
                        _FurTouchStrength           ("", Range(0, 1)) = 0

        //----------------------------------------------------------------------------------------------------------------------
        // Fur Advanced
                                               _FurCull                ("", Int) = 0
                 _FurSrcBlend            ("", Int) = 1
                 _FurDstBlend            ("", Int) = 0
                 _FurSrcBlendAlpha       ("", Int) = 1
                 _FurDstBlendAlpha       ("", Int) = 10
                   _FurBlendOp             ("", Int) = 0
                   _FurBlendOpAlpha        ("", Int) = 0
                 _FurSrcBlendFA          ("", Int) = 1
                 _FurDstBlendFA          ("", Int) = 1
                 _FurSrcBlendAlphaFA     ("", Int) = 0
                 _FurDstBlendAlphaFA     ("", Int) = 1
                   _FurBlendOpFA           ("", Int) = 4
                   _FurBlendOpAlphaFA      ("", Int) = 4
                                             _FurZClip               ("", Int) = 1
                                             _FurZWrite              ("", Int) = 1
           _FurZTest               ("", Int) = 4
                                              _FurStencilRef          ("", Range(0, 255)) = 0
                                              _FurStencilReadMask     ("", Range(0, 255)) = 255
                                              _FurStencilWriteMask    ("", Range(0, 255)) = 255
           _FurStencilComp         ("", Float) = 8
                 _FurStencilPass         ("", Float) = 0
                 _FurStencilFail         ("", Float) = 0
                 _FurStencilZFail        ("", Float) = 0
                                                        _FurOffsetFactor        ("", Float) = 0
                                                        _FurOffsetUnits         ("", Float) = 0
                                          _FurColorMask           ("", Int) = 15
                                             _FurAlphaToMask         ("", Int) = 1
    }

    SubShader
    {
        Tags {"RenderType" = "TransparentCutout" "Queue" = "AlphaTest"}
        UsePass "Hidden/lilToonFurCutout/FORWARD_FUR"
        UsePass "Hidden/lilToonFurCutout/FORWARD_ADD_FUR"
    }
    Fallback "Unlit/Texture"

    CustomEditor "lilToon.lilToonInspector"
}

