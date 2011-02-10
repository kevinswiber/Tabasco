using System;
using System.Diagnostics;
using System.Reflection;

namespace Tabasco.Plumbing
{
    public class StackTracer
    {
        public static string GetPreviousMethodName(MethodBase currentMethod)
        {
            var methodName = string.Empty;
            try
            {
                var sTrace = new StackTrace(true);

                //loop through all the stack frames
                for (var frameCount = 0; frameCount < sTrace.FrameCount; frameCount++)
                {
                    var sFrame = sTrace.GetFrame(frameCount);
                    var thisMethod = sFrame.GetMethod();

                    //If the Type in the frame is the type that is being searched
                    if (thisMethod != currentMethod)
                    {
                        continue;
                    }

                    if (frameCount + 1 <= sTrace.FrameCount)
                    {
                        var count = 1;
                        do
                        {
                            var prevFrame = sTrace.GetFrame(frameCount + count);
                            var prevMethod = prevFrame.GetMethod();
                            methodName = prevMethod.ReflectedType + "." + prevMethod.Name;
                            count++;
                            //get the method and its parameter info
                            //then exit out of the for loop
                        } while (methodName == ".CallSite.Target" ||
                                 methodName == "System.Dynamic.UpdateDelegates.UpdateAndExecute2");
                    }
                    break;
                }
            }
            catch (Exception)
            {
                //swallow all exceptions this may encounter...this is informational and mroe for convenience anyways
                return string.Empty;
            }
            return methodName;
        }
    }
}