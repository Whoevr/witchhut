﻿namespace WitchHutSearch.Generator.Features;

public abstract class FeatureLocator
{
    public static FeatureLocator WitchHut { get; } = new WitchHutLocator();

    private FeatureLocator()
    {
    }

    public abstract Pos GetPosition(ulong seed, Pos region);

    private sealed class WitchHutLocator : FeatureLocator
    {
        public override Pos GetPosition(ulong seed, Pos region)
        {
            const ulong k = 0x5deece66d;
            const ulong mask = (1UL << 48) - 1;
            const ulong b = 0xb;

            seed = seed + (ulong)region.X * 341873128712UL + (ulong)region.Z * 132897987541UL + 14357620;
            seed ^= k;
            seed = (seed * k + b) & mask;

            var pos = new DeferredPos();
            pos.X = (int)(seed >> 17) % 24;
            pos.X = (int)(((ulong)region.X * 32 + (ulong)pos.X) << 4);
            seed = (seed * k + b) & mask;
            pos.Z = (int)(seed >> 17) % 24;
            pos.Z = (int)(((ulong)region.Z * 32 + (ulong)pos.Z) << 4);
            
            return pos;
        }
    }
}