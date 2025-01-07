// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "My/Water2D"
{
Properties{
    _TileSheet("Tile_Sheet", 2D) = "white" {}
    _Waves("Wave", 2D) = "white" {}
}
SubShader{
    Pass {
        CGPROGRAM

        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"

        uniform sampler2D _TileSheet;
        sampler2D _Waves;

        uniform float2 _MapSize;

        struct vertexInput {
            float4 vertex : POSITION;
            float2 texcoord: TEXCOORD0;
            float4 tilecoord : TEXCOORD1;
        };

        struct vertexOutput {
            float4 pos : SV_POSITION;
            float2 tex: TEXCOORD0;
            float2 tileIndex : VECTOR;
        };

        vertexOutput vert(vertexInput v)
        {
            vertexOutput output;

            output.pos = UnityObjectToClipPos(v.vertex);
            output.tex = v.texcoord;
            output.tileIndex = v.tilecoord.xy; // <-- Using UV2 might be a bit hacky
                                               //     to get the tile index but it 
                                               //     seemed better than creating a 
                                               //     texture to do it...

            return output;
        }

        float4 frag(vertexOutput i) : SV_Target
        {
            int xTile = (i.tileIndex.x * 16);
            int yTile = (i.tileIndex.y * 16);

            float2 uv = float2(xTile, yTile + 1) / 16;

            float xoffset = frac(i.tex.x * _MapSize.x) / 16;
            float yoffset = frac(i.tex.y * _MapSize.y) / 16;

            uv += float2(xoffset, -yoffset);

            uv.y = 1.0 - uv.y;
            float4 waves = tex2D(_Waves, uv + _Time.x );
            return tex2D(_TileSheet, uv + waves.xy * 0.1);
        }

        ENDCG
    }
}
    }