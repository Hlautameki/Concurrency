using System;

namespace Chapter2
{
    public class Listing2_8_Osobny_generator_liczb_losowych_na_wątek : Listing2_7_Problemy_z_generowaniem_liczb_pseudolosowych
    {
        protected override Random GetGenerator()
        {
            return new Random(Random.Next() & DateTime.Now.Millisecond);
        }
    }
}
