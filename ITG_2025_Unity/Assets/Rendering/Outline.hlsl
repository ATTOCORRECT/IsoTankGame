TEXTURE2D(_CameraColorTexture);
SAMPLER(sampler_CameraColorTexture);
float4 _CameraColorTexture_TexelSize;

TEXTURE2D(_CameraDepthTexture);
SAMPLER(sampler_CameraDepthTexture);

TEXTURE2D(_CameraDepthNormalsTexture);
SAMPLER(sampler_CameraDepthNormalsTexture);

TEXTURE2D(_CameraNormalsTexture);
SAMPLER(sampler_CameraNormalsTexture);



float3 DecodeNormal(float4 enc)
{
    float kScale = 1.7777;
    float3 nn = enc.xyz*float3(2*kScale,2*kScale,0) + float3(-kScale,-kScale,1);
    float g = 2.0 / dot(nn.xyz,nn.xyz);
    float3 n;
    n.xy = g*nn.xy;
    n.z = g-1;
    return n;
}

float getDepth(float2 UV){
    return SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, UV).r;
}

float3 getNormal(float2 UV){
    return DecodeNormal(SAMPLE_TEXTURE2D(_CameraDepthNormalsTexture, sampler_CameraDepthNormalsTexture, UV)) / 2.0 + 0.5;
}

float depthEdgeIndicator(float2 UV, float side)
{
    float2 Texel = (1.0) / float2(_CameraColorTexture_TexelSize.z, _CameraColorTexture_TexelSize.w);
    float depth = getDepth(UV);
    float edgeHorizontal = abs(clamp((getDepth(UV + float2( 1 * Texel.x, 0)) + getDepth(UV + float2(-1 * Texel.x, 0))) / 2 - depth,-1.0 + side, 0.0 + side)); // horizontal
    float edgeVertical   = abs(clamp((getDepth(UV + float2( 0, 1 * Texel.y)) + getDepth(UV + float2( 0,-1 * Texel.y))) / 2 - depth,-1.0 + side, 0.0 + side)); // vertical
    float edge = max(edgeHorizontal, edgeVertical);
    return edge;
}

void Outline_float(float2 UV, float side, out float4 Out)
{
    Out = depthEdgeIndicator(UV, side);
}