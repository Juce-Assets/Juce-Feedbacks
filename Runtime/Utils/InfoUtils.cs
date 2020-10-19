using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Juce.Feedbacks
{
    public static class InfoUtils
    {
        public const string Separator = "|";

        public static string FormatInfo(ref List<string> infoList)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for(int i = 0; i < infoList.Count; ++i)
            {
                if (i > 0)
                {
                    stringBuilder.Append($" {Separator} ");
                }

                stringBuilder.Append(infoList[i]);
            }

            return stringBuilder.ToString();
        }

        public static void GetTimingInfo(ref List<string> infoList,  float delay, float duration)
        {
            if(delay > 0)
            {
                infoList.Add($"Delay: {delay}s");
            }

            infoList.Add($"Duration: {duration}s");
        }

        public static void GetTimingInfo(ref List<string> infoList, float delay)
        {
            if (delay > 0)
            {
                infoList.Add($"Delay: {delay}s");
            }
        }

        public static void GetInteractableInfo(ref List<string> infoList, bool interactable, bool blocksRaycast)
        {
            infoList.Add($"Interactable: {interactable}");
            infoList.Add($"BlocksRaycast: {blocksRaycast}");
        }

        public static void GetCoordinatesSpaceInfo(ref List<string> infoList, CoordinatesSpace coordinatesSpace)
        {
            infoList.Add($"{coordinatesSpace}");
        }

        public static void GetStartEndVector3PropertyInfo(ref List<string> infoList, StartEndVector3Property startEndVector3)
        {
            string startString = string.Empty;

            if(startEndVector3.UseStartX || startEndVector3.UseStartY || startEndVector3.UseStartZ)
            {
                startString += $"Start [";
            }

            if(startEndVector3.UseStartX)
            {
                startString += $" x:{startEndVector3.StartValueX}";
            }

            if (startEndVector3.UseStartY)
            {
                startString += $" y:{startEndVector3.StartValueX}";
            }

            if (startEndVector3.UseStartZ)
            {
                startString += $" z:{startEndVector3.StartValueX}";
            }

            if (startEndVector3.UseStartX || startEndVector3.UseStartY || startEndVector3.UseStartZ)
            {
                startString += " ]";

                infoList.Add(startString);
            }

            string endString = string.Empty;

            if (startEndVector3.UseEndX || startEndVector3.UseEndY || startEndVector3.UseEndZ)
            {
                endString += $"End [";
            }

            if (startEndVector3.UseEndX)
            {
                endString += $" x:{startEndVector3.EndValueX}";
            }

            if (startEndVector3.UseEndY)
            {
                endString += $" y:{startEndVector3.EndValueY}";
            }

            if (startEndVector3.UseStartZ)
            {
                endString += $" z:{startEndVector3.EndValueZ}";
            }

            if (startEndVector3.UseStartX || startEndVector3.UseStartY || startEndVector3.UseStartZ)
            {
                endString += " ]";

                infoList.Add(endString);
            }
        }

        public static void GetStartEndUnitFloatPropertyInfo(ref List<string> infoList, StartEndUnitFloatProperty startEndUnitFloat)
        {
            if (startEndUnitFloat.UseStartValue)
            {
                infoList.Add($"Start: {startEndUnitFloat.StartValue}");
            }

            infoList.Add($"End: {startEndUnitFloat.EndValue}");
        }

        public static void GetStartEndColorPropertyInfo(ref List<string> infoList, StartEndColorProperty startEndColorProperty) 
        {
            string startString = string.Empty;

            if (startEndColorProperty.UseStartColor || startEndColorProperty.UseStartAlpha)
            {
                startString += $"Start [";
            }

            if (startEndColorProperty.UseStartColor)
            {
                startString += $" {ColorNoAlphaToString(startEndColorProperty.StartColor)}";
            }

            if (startEndColorProperty.UseStartAlpha)
            {
                startString += $" a:{startEndColorProperty.StartAlpha}";
            }

            if (startEndColorProperty.UseStartColor || startEndColorProperty.UseStartAlpha)
            {
                startString += $" ]";

                infoList.Add(startString);
            }

            string endString = string.Empty;

            if (startEndColorProperty.UseEndColor || startEndColorProperty.UseEndAlpha)
            {
                endString += $"End [";
            }

            if (startEndColorProperty.UseEndColor)
            {
                endString += $" {ColorNoAlphaToString(startEndColorProperty.EndColor)}";
            }

            if (startEndColorProperty.UseEndAlpha)
            {
                endString += $" a:{startEndColorProperty.EndAlpha}";
            }

            if (startEndColorProperty.UseEndColor || startEndColorProperty.UseEndAlpha)
            {
                endString += $" ]";

                infoList.Add(endString);
            }
        }

        public static string ColorNoAlphaToString(Color color)
        {
            return $"r:{color.r} g:{color.g} b:{color.b}";
        }
    }
}
