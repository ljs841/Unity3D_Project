Shader "Hidden/GrayScale"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "" {}
    }
    SubShader
    { 
		Tags {  "Queue" = "Transparent" "RenderType" = "Transparent" }
        // No culling or depth
        Cull Off ZWrite Off ZTest LEqual

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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
			float4 _MainTex_ST;
            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
         
                return o;
            }

            sampler2D _MainTex;

            float4 frag (v2f i) : SV_Target
            {
				float4 col = tex2D(_MainTex, i.uv);
				float3 grayScaleValue = float3(0.3f, 0.59f, 0.11f);
                // just invert the colors
				col.rgb = lerp(col.rgb, dot(col.rgb, float3(0.3, 0.59, 0.11)), 1.0);
				col.a = col.a;
				//col.rgb = dot(col.rgb , grayScaleValue);
				clip(col.a - 0.001);
                return col;
            }
            ENDCG
        }
    }
}
