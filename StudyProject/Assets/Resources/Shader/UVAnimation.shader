Shader "Hidden/UVAnimation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_TexelSize("Texture Size" , VECTOR) = (0,0,0,0)
    }
    SubShader
    {
		Tags {  "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" "PreviewType" = "Plane" "CanUseSpriteAtlas" = "true" }
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"
			

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
			float4 _TexelSize;
			float4 _MainTex_ST;
            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);

				o.uv = v.uv;
				
                return o;
            }

            sampler2D _MainTex;
            fixed4 frag (v2f i) : SV_Target
            {

				float4 col = tex2D(_MainTex, i.uv);
				float4 scol = tex2D(_MainTex, i.uv * -1);
				
				return col;
            }
            ENDCG
        }
    }
}
