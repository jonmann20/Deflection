Shader "Vertex/VertexLit Colored Alpha" {

Properties {

    _Color ("Main Color", Color) = (1,1,1,1)

    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}

    _BumpMap ("Illumin (A)", 2D) = "bump" {}

}

 

SubShader {

    ZWrite On

    Alphatest Greater 0

    Tags {Queue=Transparent}

    Blend SrcAlpha OneMinusSrcAlpha 

    ColorMask RGB

    Pass {

        ColorMaterial AmbientAndDiffuse

        Lighting On

        SetTexture [_Bump] {

            Combine texture * constant DOUBLE, previous * primary

        } 

        SetTexture [_MainTex] {

            constantColor [_Color]

            Combine texture * primary DOUBLE, previous * constant

        }       

    }

    Pass {

        ColorMaterial AmbientAndDiffuse

        Lighting On

        SetTexture [_MainTex] {

            constantColor [_Color]

            Combine texture * constant DOUBLE, previous * texture

        } 

        

    }

}

 

Fallback "Alpha/VertexLit", 1

}