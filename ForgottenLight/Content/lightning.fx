sampler s0;

texture lightMask;
sampler lightSampler = sampler_state { Texture = <lightMask>; };

float4 main(float4 texCoord : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, texCoord);
	float4 lightColor = tex2D(lightSampler, texCoord);
	return color * lightColor;
}

technique Technique0
{
	pass Pass0
	{
		PixelShader = compile ps_2_0 main();
	}
}
