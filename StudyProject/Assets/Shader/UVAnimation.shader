Shader "Custom/UVAnimation"
{
	
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		[PerRendererData] _SpriteRect  ("SpriteRect" , Vector) = (0,0,0,0)
		[PerRendererData] _Speed("ScrollSpeed" , float) = 1
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
			

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
            };
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
                return o;
            }

			sampler2D _MainTex;
			float _Speed;
			float4 _SpriteRect;
            fixed4 frag (v2f i) : SV_Target
            {
				float offset = i.uv.x + _SpriteRect.z * _Speed;
				if (offset >= _SpriteRect.x + _SpriteRect.z)
				{
					offset = offset - _SpriteRect.z;
				}
				i.uv = float2(offset , i.uv.y);
				float4 col = tex2D(_MainTex, i.uv);
				return col;
            }
            ENDCG
        }
    }
}
