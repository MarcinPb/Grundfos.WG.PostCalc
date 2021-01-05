using System.Configuration;

namespace Grundfos.WG2SVG.Configuration
{
    public class Transformation : ConfigurationElement
    {
        [ConfigurationProperty("sequenceNumber")]
        public int SequenceNumber
        {
            get
            {
                return (int)this["sequenceNumber"];
            }

            set
            {
                this["sequenceNumber"] = value;
            }
        }

        [ConfigurationProperty("rotateByDegrees")]
        public double RotateByDegrees
        {
            get
            {
                return (double)this["rotateByDegrees"];
            }

            set
            {
                this["rotateByDegrees"] = value;
            }
        }

        [ConfigurationProperty("transformationType")]
        public TransformationTypes TransformationType
        {
            get
            {
                return (TransformationTypes)this["transformationType"];
            }

            set
            {
                this["transformationType"] = value;
            }
        }

        [ConfigurationProperty("scaleX")]
        public double ScaleX
        {
            get
            {
                return (double)this["scaleX"];
            }

            set
            {
                this["scaleX"] = value;
            }
        }

        [ConfigurationProperty("scaleY")]
        public double ScaleY
        {
            get
            {
                return (double)this["scaleY"];
            }

            set
            {
                this["scaleY"] = value;
            }
        }

        [ConfigurationProperty("moveX")]
        public double MoveX
        {
            get
            {
                return (double)this["moveX"];
            }

            set
            {
                this["moveX"] = value;
            }
        }

        [ConfigurationProperty("moveY")]
        public double MoveY
        {
            get
            {
                return (double)this["moveY"];
            }

            set
            {
                this["moveY"] = value;
            }
        }

        [ConfigurationProperty("canvasWidth")]
        public double CanvasWidth
        {
            get
            {
                return (double)this["canvasWidth"];
            }

            set
            {
                this["canvasWidth"] = value;
            }
        }

        [ConfigurationProperty("canvasHeight")]
        public double CanvasHeight
        {
            get
            {
                return (double)this["canvasHeight"];
            }

            set
            {
                this["canvasHeight"] = value;
            }
        }

        public override string ToString()
        {
            return this.TransformationType.ToString();
        }
    }
}
