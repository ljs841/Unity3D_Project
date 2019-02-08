Shader "Hidden/UVAnimation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_dd ("cd" , Float) = 0
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
                float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
            };
			float _dd;
            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
				//frac 소수점만 가져온다 0보다 크고 1보다 작은값이 된다
				o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            fixed4 frag (v2f i) : SV_Target
            {
				float gf = i.uv.x + frac(_Time.x);
				_dd = gf;
				//frac 소수점만 가져온다 0보다 크고 1보다 작은값이 된다
				//o.uv = v.uv + float2(frac(_Time.x), 0);
				if (gf > 0.999)
				{
					//gf =  (1 - i.uv.x) ;
					gf = i.uv.x;
				}
				i.uv.x = gf;
				float4 col = tex2D(_MainTex, i.uv);
				return col;
            }
            ENDCG
        }
    }
}
