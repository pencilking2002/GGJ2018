// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Gradient/Multi Palette/Angular/Inverted UV/Solid Blend" {

	//Set up the shader to receive external inputs from Unity
	Properties {
		_UVXOffset ("UV X Offset", float) = 0			//Receive input from UV coordinate X offset
		_UVYOffset ("UV Y Offset", float) = 0			//Receive input from UV coordinate Y offset
		_Angle ("Angle", float) = 0						//Receive input from rotation Angle (0..360 degrees)
		_PaletteTex ("Palette Texture", 2D) = "" {}				//Receive input from a Texture on Texture Unit 0
		_PaletteRow ("Palette Row", float) = 0			//Receive input from a float
	}

	//Define a shader
	SubShader {

		//Define what queue/order to render this shader in
		Tags {"Queue" = "Geometry" "RenderType" = "Opaque"}		//Background | Geometry | AlphaTest | Transparent | Overlay

		//Define a pass
		Pass {

			//Set up blending and other operations
			Cull Off			// Back | Front | Off - Do not cull any triangle faces
			ZTest LEqual		//Less | Greater | LEqual | GEqual | Equal | NotEqual | Always - Z-Buffer/Depth testing is off
			ZWrite On			//On | Off - Z coordinates from pixel positions will not be written to the Z/Depth buffer
			AlphaTest Off		//0.0	//Less | Greater | LEqual | GEqual | Equal | NotEqual | Always   (also 0.0 (float value) | [_AlphaTestThreshold]) - All pixels will continue through the graphics pipeline because alpha testing is Off
			Lighting Off		//On | Off - Lighting will not be calculated or applied
			ColorMask RGBA		//RGBA | RGB | A | 0 | any combination of R, G, B, A - Color channels allowed to be modified in the backbuffer are: RGBA
			//BlendOp	//Add	// Min | Max | Sub | RevSub - BlendOp is not being used and will default to an Add operation when combining the source and destination parts of the blend mode
			Blend Off			//SrcAlpha OneMinusSrcAlpha			//SrcFactor DstFactor (also:, SrcFactorA DstFactorA) = One | Zero | SrcColor | SrcAlpha | DstColor | DstAlpha | OneMinusSrcColor | OneMinusSrcAlpha | OneMinusDstColor | OneMinusDstAlpha - Blending between shader output and the backbuffer will use blend mode 'Alpha Blend'
								//Blend SrcAlpha OneMinusSrcAlpha     = Alpha blending
								//Blend One One                       = Additive
								//Blend OneMinusDstColor One          = Soft Additive
								//Blend DstColor Zero                 = Multiplicative
								//Blend DstColor SrcColor             = 2x Multiplicative

			CGPROGRAM						//Start a program in the CG language
			#pragma target 2.0				//Run this shader on at least Shader Model 2.0 hardware (e.g. Direct3D 9)
			#pragma fragment frag			//The fragment shader is named 'frag'
			#pragma vertex vert				//The vertex shader is named 'vert'
			#include "UnityCG.cginc"		//Include Unity's predefined inputs and macros

			//Unity variables to be made accessible to Vertex and/or Fragment shader
			uniform sampler2D _PaletteTex;					//Define _PaletteTex from Texture Unit 0 to be sampled in 2D
			//uniform float4 _PaletteTex_ST;					//Use the Float _PaletteTex_ST to pass the Offset and Tiling for the texture(s)
			uniform float _UVXOffset;
			uniform float _UVYOffset;
			uniform float _Angle;
			uniform float _PaletteRow;
			
			//Data structure communication from Unity to the vertex shader
			//Defines what inputs the vertex shader accepts
			struct AppData {
				float4 vertex : POSITION;					//Receive vertex position
				half2 texcoord : TEXCOORD0;					//Receive texture coordinates
				//half2 texcoord1 : TEXCOORD1;				//Receive texture coordinates
							//fixed4 color : COLOR;						//Receive vertex colors
			};

			//Data structure for communication from vertex shader to fragment shader
			//Defines what inputs the fragment shader accepts
			struct VertexToFragment {
				float4 pos : POSITION;						//Send fragment position to fragment shader
				half2 uv : TEXCOORD0;						//Send interpolated texture coordinate to fragment shader
				//half2 uv2 : TEXCOORD1;					//Send interpolated texture coordinate to fragment shader
							//fixed4 color : COLOR;						//Send interpolated gouraud-shaded vertex color to fragment shader
			};

			//Vertex shader
			VertexToFragment vert(AppData v) {
				VertexToFragment o;							//Create a data structure to pass to fragment shader
				o.pos = UnityObjectToClipPos(v.vertex);		//Include influence of Modelview + Projection matrices
				//o.uv = TRANSFORM_TEX(v.texcoord,_PaletteTex);//Send texture coords from unit 0 to fragment shader
				//o.uv = v.texcoord.xy;
				v.texcoord.xy = half2((v.texcoord.x+_UVXOffset),(v.texcoord.y+_UVYOffset));
				float ang = _Angle*-0.01745329;				//0.01745329 is conversion of 360.0/(2*PI) as 1.0/(360.0/(2*PI)) to convert angle to radians
 	            float sinX = sin(ang);
    	        float cosX = cos(ang);
        	    float sinY = sin(ang);
            	float2x2 rotationMatrix = float2x2(cosX,-sinX,sinY,cosX);	//Position and rotate
           		o.uv = mul(v.texcoord.xy, rotationMatrix);
							//o.uv2 = v.texcoord1.xy;					//Send texture coords from unit 1 to fragment shader
							//o.color = v.color;						//Send interpolated vertex color to fragment shader
							//o.color = _Color;							//Send solid color to fragment shader
				return o;									//Transmit data to the fragment shader
			}

			//Fragment shader
			fixed4 frag(VertexToFragment i) : COLOR {
				fixed4 tex=tex2D(_PaletteTex, half2(lerp(1.0,0,(atan2(i.uv.y,i.uv.x)*0.1591549)+0.5),_PaletteRow) );	//Output paletted angular gradient (0.1591549 is 1.0/(2*PI))
				return tex;
			}

			ENDCG							//End of CG program

		}
	}
}

//Copyright (c) 2013 Paul West/Venus12 LLC