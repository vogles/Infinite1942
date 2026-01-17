using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infinite1942
{
    public class Transform
    {
        private Vector3 _worldPosition = Vector3.Zero;
        private Vector3 _worldScale = Vector3.One;
        private Quaternion _worldRotation = Quaternion.Identity;

        public Matrix WorldMatrix
        {
            get
            {
                return Matrix.CreateScale(_worldScale) *
                    Matrix.CreateFromQuaternion(_worldRotation) *
                    Matrix.CreateTranslation(_worldPosition);
            }
        }

        public void Translate(float x, float y, float z)
        {
            Translate(new Vector3(x, y, z));
        }

        public void Translate(Vector3 translation)
        {
            _worldPosition += translation;
        }

        public void Rotate(Vector3 eulerAngles)
        {
            var rotation = Quaternion.CreateFromYawPitchRoll(eulerAngles.Y, eulerAngles.X, eulerAngles.Z);

            _worldRotation *= Quaternion.Inverse(_worldRotation) * rotation * _worldRotation;
        }
    }
}
