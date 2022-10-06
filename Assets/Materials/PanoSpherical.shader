Shader "Kojack/PanoSpherical"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			Cull off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			//#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float3 world : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.world = mul(unity_ObjectToWorld, v.vertex)*float3(-1,-1,1);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 p = normalize(i.world);
				float r = sqrt(dot(i.world, i.world));
				float theta = atan2(i.world.z, i.world.x) / (3.14159265*2.0) + 0.5;
				float phi = acos(i.world.y / r) / 3.14159265;
				return tex2Dlod(_MainTex, float4(theta,phi,0.0,0.0));
			}
			ENDCG
		}
	}
}
