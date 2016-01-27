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

            switcher.ChoosePath(feature,
                DoNewThing,
                DoOldThing,
                "Eric");

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

        private static void DoNewThing(string name)
        {
            Console.WriteLine("Doing new thing with {0}", name);
        }

        private static void DoOldThing(string name)
        {
            Console.WriteLine("Doing old thing with {0}", name);
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

        public void ChoosePath<T>(string feature,
            Action<T> enabledAction,
            Action<T> disabledAction,
            T value)
        {
            if (!_flags.ContainsKey(feature) || !_flags[feature])
            {
                disabledAction(value);
            }
            else
            {
                enabledAction(value);
            }
        }
    }
}
