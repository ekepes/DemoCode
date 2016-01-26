using System;
using System.Collections.Generic;

namespace FeatureTogglesThoughtExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            var switcher = new Switcher();
            var feature = "newFeature";

            switcher.EnableFeature(feature);

            switcher.ChoosePath(feature,
                DoNewThing,
                DoOldThing);

            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        private static void DoNewThing()
        {
            Console.WriteLine("Doing new thing");
        }

        private static void DoOldThing()
        {
            Console.WriteLine("Doing old thing");
        }
    }

    public class Switcher
    {
        private readonly Dictionary<string, bool> _flags = new Dictionary<string, bool>();

        public void EnableFeature(string feature)
        {
            SetFeatureFlag(feature, true);
        }

        public void DisableFeature(string feature)
        {
            SetFeatureFlag(feature, false);
        }

        private void SetFeatureFlag(string feature, bool enabled)
        {
            if (_flags.ContainsKey(feature))
            {
                _flags[feature] = enabled;
            }
            else
            {
                _flags.Add(feature, enabled);
            }
            
        }

        public void ChoosePath(string feature,
            Action enabledAction,
            Action disabledAction)
        {
            if (!_flags.ContainsKey(feature) || !_flags[feature])
            {
                disabledAction();
            }
            else
            {
                enabledAction();
            }
        }
    }
}
