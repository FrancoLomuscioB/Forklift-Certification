void ToonShading_Float(in float3 Normal, in float ToonRampSmoothness, in float3 ClipSpacePos, in float3 WorldPos, in float4 ToonRampTinting
	in float ToonRampOffset, out float3 ToonRampOutput, out float3 direction)
{
   #ifdef SHADERGRAPH_PREVIEW
	ToonRampOutput = float3(0.5, 0.5, 0);
	Direction = float3(0.5, 0.5, 0);
   #else
    #if SHADOWS_SCREEN
	half4 shadowCoord = ComputerScreenPos(ClipSpacePos);
    #else
	half4 shadowcord =
}