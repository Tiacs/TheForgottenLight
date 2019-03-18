sampler s0;

texture lightMask;
sampler lightSampler = sampler_state { Texture = <lightMask>; };

float time;

// float4 main(float4 position : SV_POSITION, float4 c : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
float4 main(float4 texCoord : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, texCoord);
	float4 lightColor = tex2D(lightSampler, texCoord);
	// clip(-2);
	return color * lightColor; // *(1 + cos(time*.5f)*.5f) * (1 + cos(2.2321*time)*.5f) * (1 + cos(5.2321*time)*.05f);
}

technique Technique0
{
	pass Pass0
	{
		PixelShader = compile ps_2_0 main();
	}
}
