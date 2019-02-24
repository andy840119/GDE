using GDEdit.Utilities.Attributes;
using GDEdit.Utilities.Enumerations.GeometryDash;
using GDEdit.Utilities.Information.GeometryDash;
using GDEdit.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a custom particle object.</summary>
    [FutureProofing("2.2")]
    public class CustomParticleObject : SpecialObject
    {
        /// <summary>Represents the infinite duration constant for the Duration property.</summary>
        public const double InfiniteDuration = -1;

        private byte maxParticles, emission, texture;
        private float duration;
        private Point posVar, gravity;
        private SymmetricalRange<int> startSize, endSize, startSpin, endSpin;
        private SymmetricalRange<float> lifetime, angle, speed, accelRad, accelTan, fadeIn, fadeOut;
        private SymmetricalRange<Color> start, end;
        
        #region Motion
        /// <summary>The grouping of the custom particles.</summary>
        public CustomParticleGrouping Grouping { get; set; }
        // TODO: Figure out what this does
        /// <summary>The property 1 of the custom particles.</summary>
        public CustomParticleProperty1 Property1 { get; set; }
        /// <summary>The maximum number of particles that will be alive simultaneously.</summary>
        public int MaxParticles
        {
            get => maxParticles;
            set => maxParticles = (byte)value;
        }
        /// <summary>The duration of the particle creation.</summary>
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The lifetime of the particle creation.</summary>
        public double Lifetime
        {
            get => lifetime.MiddleValue;
            set => lifetime.MiddleValue = (float)value;
        }
        /// <summary>The Lifetime +- property.</summary>
        public double LifetimeAdjustment
        {
            get => lifetime.MaximumDistance;
            set => lifetime.MaximumDistance = (float)value;
        }
        /// <summary>The Emission property (unknown functionality).</summary>
        public int Emission
        {
            get => emission;
            set => emission = (byte)value;
        }
        /// <summary>The angle of the particles and the center.</summary>
        public double Angle
        {
            get => angle.MiddleValue;
            set => angle.MiddleValue = (float)value;
        }
        /// <summary>The Angle +- property.</summary>
        public double AngleAdjustment
        {
            get => angle.MaximumDistance;
            set => angle.MaximumDistance = (float)value;
        }
        /// <summary>The speed at which the particles move.</summary>
        public double Speed
        {
            get => speed.MiddleValue;
            set => speed.MiddleValue = (float)value;
        }
        /// <summary>The Speed +- property.</summary>
        public double SpeedAdjustment
        {
            get => speed.MaximumDistance;
            set => speed.MaximumDistance = (float)value;
        }
        /// <summary>The PosVarX property (unknown functionality).</summary>
        public double PosVarX
        {
            get => posVar.X;
            set => posVar.X = (float)value;
        }
        /// <summary>The PosVarY +- property (unknown functionality).</summary>
        public double PosVarY
        {
            get => posVar.Y;
            set => posVar.Y = (float)value;
        }
        /// <summary>The GravityX property (unknown functionality).</summary>
        public double GravityX
        {
            get => gravity.X;
            set => gravity.X = (float)value;
        }
        /// <summary>The GravityY +- property (unknown functionality).</summary>
        public double GravityY
        {
            get => gravity.Y;
            set => gravity.Y = (float)value;
        }
        /// <summary>The AccelRad property (unknown functionality).</summary>
        public double AccelRad
        {
            get => accelRad.MiddleValue;
            set => accelRad.MiddleValue = (float)value;
        }
        /// <summary>The AccelRad +- property (unknown functionality).</summary>
        public double AccelRadAdjustment
        {
            get => accelRad.MaximumDistance;
            set => accelRad.MaximumDistance = (float)value;
        }
        /// <summary>The AccelTan property (unknown functionality).</summary>
        public double AccelTan
        {
            get => accelTan.MiddleValue;
            set => accelTan.MiddleValue = (float)value;
        }
        /// <summary>The AccelTan +- property (unknown functionality).</summary>
        public double AccelTanAdjustment
        {
            get => accelTan.MaximumDistance;
            set => accelTan.MaximumDistance = (float)value;
        }
        #endregion
        #region Visual
        /// <summary>The size during the start of the particle's life.</summary>
        public int StartSize
        {
            get => startSize.MiddleValue;
            set => startSize.MiddleValue = value;
        }
        /// <summary>The StartSize +- property.</summary>
        public int StartSizeAdjustment
        {
            get => startSize.MaximumDistance;
            set => startSize.MaximumDistance = value;
        }
        /// <summary>The size during the end of the particle's life.</summary>
        public int EndSize
        {
            get => endSize.MiddleValue;
            set => endSize.MiddleValue = value;
        }
        /// <summary>The EndSize +- property.</summary>
        public int EndSizeAdjustment
        {
            get => endSize.MaximumDistance;
            set => endSize.MaximumDistance = value;
        }
        /// <summary>The rotation during the start of the particle's life.</summary>
        public int StartSpin
        {
            get => startSpin.MiddleValue;
            set => startSpin.MiddleValue = value;
        }
        /// <summary>The StartSpin +- property.</summary>
        public int StartSpinAdjustment
        {
            get => startSpin.MaximumDistance;
            set => startSpin.MaximumDistance = value;
        }
        /// <summary>The rotation during the end of the particle's life.</summary>
        public int EndSpin
        {
            get => endSpin.MiddleValue;
            set => endSpin.MiddleValue = value;
        }
        /// <summary>The EndSpin +- property.</summary>
        public int EndSpinAdjustment
        {
            get => endSpin.MaximumDistance;
            set => endSpin.MaximumDistance = value;
        }
        /// <summary>The alpha value of the color during the start of the particle's life.</summary>
        public double StartA
        {
            get => start.MiddleValue.A;
            set => start.MiddleValue.A = (float)value;
        }
        /// <summary>The Start_A +- property.</summary>
        public double StartAAdjustment
        {
            get => start.MaximumDistance.A;
            set => start.MaximumDistance.A = (float)value;
        }
        /// <summary>The red value of the color during the start of the particle's life.</summary>
        public double StartR
        {
            get => start.MiddleValue.R;
            set => start.MiddleValue.R = (float)value;
        }
        /// <summary>The Start_R +- property.</summary>
        public double StartRAdjustment
        {
            get => start.MaximumDistance.R;
            set => start.MaximumDistance.R = (float)value;
        }
        /// <summary>The green value of the color during the start of the particle's life.</summary>
        public double StartG
        {
            get => start.MiddleValue.G;
            set => start.MiddleValue.G = (float)value;
        }
        /// <summary>The Start_G +- property.</summary>
        public double StartGAdjustment
        {
            get => start.MaximumDistance.G;
            set => start.MaximumDistance.G = (float)value;
        }
        /// <summary>The blue value of the color during the start of the particle's life.</summary>
        public double StartB
        {
            get => start.MiddleValue.B;
            set => start.MiddleValue.B = (float)value;
        }
        /// <summary>The Start_B +- property.</summary>
        public double StartBAdjustment
        {
            get => start.MaximumDistance.B;
            set => start.MaximumDistance.B = (float)value;
        }
        /// <summary>The alpha value of the color during the end of the particle's life.</summary>
        public double EndA
        {
            get => end.MiddleValue.A;
            set => end.MiddleValue.A = (float)value;
        }
        /// <summary>The End_A +- property.</summary>
        public double EndAAdjustment
        {
            get => end.MaximumDistance.A;
            set => end.MaximumDistance.A = (float)value;
        }
        /// <summary>The red value of the color during the end of the particle's life.</summary>
        public double EndR
        {
            get => end.MiddleValue.R;
            set => end.MiddleValue.R = (float)value;
        }
        /// <summary>The End_R +- property.</summary>
        public double EndRAdjustment
        {
            get => end.MaximumDistance.R;
            set => end.MaximumDistance.R = (float)value;
        }
        /// <summary>The green value of the color during the end of the particle's life.</summary>
        public double EndG
        {
            get => end.MiddleValue.G;
            set => end.MiddleValue.G = (float)value;
        }
        /// <summary>The End_G +- property.</summary>
        public double EndGAdjustment
        {
            get => end.MaximumDistance.G;
            set => end.MaximumDistance.G = (float)value;
        }
        /// <summary>The blue value of the color during the end of the particle's life.</summary>
        public double EndB
        {
            get => end.MiddleValue.B;
            set => end.MiddleValue.B = (float)value;
        }
        /// <summary>The End_B +- property.</summary>
        public double EndBAdjustment
        {
            get => end.MaximumDistance.B;
            set => end.MaximumDistance.B = (float)value;
        }
        #endregion
        #region Extra
        /// <summary>The Fade In property.</summary>
        public double FadeIn
        {
            get => fadeIn.MiddleValue;
            set => fadeIn.MiddleValue = (float)value;
        }
        /// <summary>The Fade In +- property.</summary>
        public double FadeInAdjustment
        {
            get => fadeIn.MaximumDistance;
            set => fadeIn.MaximumDistance = (float)value;
        }
        /// <summary>The Fade Out property.</summary>
        public double FadeOut
        {
            get => fadeOut.MiddleValue;
            set => fadeOut.MiddleValue = (float)value;
        }
        /// <summary>The Fade Out +- property.</summary>
        public double FadeOutAdjustment
        {
            get => fadeOut.MaximumDistance;
            set => fadeOut.MaximumDistance = (float)value;
        }
        /// <summary>Represents the Additive property of the custom particle object.</summary>
        public bool Additive
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }
        /// <summary>Represents the Start Size = End property of the custom particle object.</summary>
        public bool StartSizeEqualsEnd
        {
            get => SpecialObjectBools[1];
            set => SpecialObjectBools[1] = value;
        }
        /// <summary>Represents the Start Spin = End property of the custom particle object.</summary>
        public bool StartSpinEqualsEnd
        {
            get => SpecialObjectBools[2];
            set => SpecialObjectBools[2] = value;
        }
        /// <summary>Represents the Start Radius = End property of the custom particle object.</summary>
        public bool StartRadiusEqualsEnd
        {
            get => SpecialObjectBools[3];
            set => SpecialObjectBools[3] = value;
        }
        /// <summary>Represents the Start Rotation Is Dir property of the custom particle object.</summary>
        public bool StartRotationIsDir
        {
            get => SpecialObjectBools[4];
            set => SpecialObjectBools[4] = value;
        }
        /// <summary>Represents the Dynamic Rotation property of the custom particle object.</summary>
        public bool DynamicRotation
        {
            get => SpecialObjectBools[5];
            set => SpecialObjectBools[5] = value;
        }
        /// <summary>Represents the Use Object Color property of the custom particle object.</summary>
        public bool UseObjectColor
        {
            get => SpecialObjectBools[6];
            set => SpecialObjectBools[6] = value;
        }
        /// <summary>Represents the Uniform Object Color property of the custom particle object.</summary>
        public bool UniformObjectColor
        {
            get => SpecialObjectBools[7];
            set => SpecialObjectBools[7] = value;
        }
        #endregion

        /// <summary>The texture of the particles.</summary>
        public int Texture
        {
            get => texture;
            set => texture = (byte)value;
        }

        /// <summary>Initializes a new empty instance of the <seealso cref="CustomParticleObject"/> class. For internal use only.</summary>
        private CustomParticleObject() : base() { }

        /// <summary>Returns a clone of this <seealso cref="CustomParticleObject"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new CustomParticleObject());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as CustomParticleObject;
            // TODO: Add cloned property information
            return base.AddClonedInstanceInformation(c);
        }
    }
}
