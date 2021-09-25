using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Test_BaseLinker.Modele
{
    public interface IFeature
    { 
    
    }

    public class FeatureModel :IFeature
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public FeatureModel()
        { 
        
        }
    }
}
