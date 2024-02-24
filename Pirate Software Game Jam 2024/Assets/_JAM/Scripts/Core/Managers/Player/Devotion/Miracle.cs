using System;

namespace Base.Core.Managers
{
    [Serializable]
    public class Miracle
    {

        public MiracleType _miracleType;
        
        public void MiracleEffect(Citizen targetCitizen) // Maybe should be moved to the mono behaviour object??? too complex logic??
        {
            switch (_miracleType)
            {
                // Temporary calculations, attribute starts at 0, miracles do basic amount, commandments add variation.
                case MiracleType.RedBasic:
                    targetCitizen.Health += 1;
                    break;
                case MiracleType.RedIntermediate:
                    targetCitizen.Health += 2;
                    break;
                case MiracleType.RedSuperior:
                    targetCitizen.Health += 5;
                    break;
                case MiracleType.BlueBasic:
                    targetCitizen.Sanity += 1;
                    break;
                case MiracleType.BlueIntermediate:
                    targetCitizen.Sanity += 2;
                    break;
                case MiracleType.BlueSuperior:
                    targetCitizen.Sanity += 5;
                    break;
                case MiracleType.GreenBasic:
                    targetCitizen.Happiness += 1;
                    break;
                case MiracleType.GreenIntermediate:
                    targetCitizen.Happiness += 2;
                    break;
                case MiracleType.GreenSuperior:
                    targetCitizen.Happiness += 5;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_miracleType), _miracleType, null);
            }
        }

    }
}