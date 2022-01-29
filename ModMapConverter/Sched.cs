using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator3
{
    public class CoroutineHandler
    {
        // classic Generator

        public static void StartUpdate()
        {
            while (true)
            {
                Update();
            }
        }

        private static void Update()
        {
            Sched.Instance.Update();
        }
    }

    // Scheduler
    public class Sched
    {
        // simple singleton
        private static Sched instance = new Sched();
        public static Sched Instance
        {
            get { return instance; }
        }

        public class Coroutine
        {
            public IEnumerator routine;
            public Coroutine waitForCoroutine;
            public bool finished = false;

            public Coroutine(IEnumerator routine) { this.routine = routine; }
        }

        List<Coroutine> coroutines = new List<Coroutine>();

        private Sched() { }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            Coroutine coroutine = new Coroutine(routine);
            coroutines.Add(coroutine);

            return coroutine;
        }

        public void Update()
        {
            foreach (Coroutine coroutine in coroutines.Reverse<Coroutine>())
            {
                if (coroutine.routine.Current is Coroutine)
                    coroutine.waitForCoroutine = coroutine.routine.Current as Coroutine;

                if (coroutine.waitForCoroutine != null && coroutine.waitForCoroutine.finished)
                    coroutine.waitForCoroutine = null;

                if (coroutine.waitForCoroutine != null)
                    continue;

                // update coroutine

                if (coroutine.routine.MoveNext())
                {
                    coroutine.finished = false;
                }
                else
                {
                    coroutines.Remove(coroutine);
                    coroutine.finished = true;
                }
            }
        }

        public static IEnumerator WaitAboutSeconds(int seconds)
        {
            // dumb timer
            int timer = DateTime.Now.Second + seconds;
            while (DateTime.Now.Second <= timer)
            {
                // pass
                yield return null;
            }

            yield break;
        }
    }
}