using System;
using System.Collections.Generic;
using System.Text;

namespace Roboarm
{
    public class Equations
    {
        public class Nilai
        {
       
            private double[] ACCELERO_VECTOR(double jari2, double ARM_LENGHT)// dipakai buat naik turun
            {
                double sudut1 = Math.Acos((jari2 * jari2) / (2 * jari2 * ARM_LENGTH));

                double sudut2 = Math.Acos((2 * (ARM_LENGTH * ARM_LENGTH)) - (jari2 * jari2) / (2 * ARM_LENGTH * ARM_LENGTH));
                sudut1 = sudut1 + Math.Asin(cont_y / jari2);
                var re = new double[sudut1, sudut2];
                return re;
            }
            private double ROTATION_VECTOR (double sudut)//dipake buat ke kanan sama kiri
            {
                return (sudut / 1 * 360)/2; // berlaku buat semua XYZ
            }
            
        }
        private double radius(double y, double z)// function nyari jari" perpindahan posisi hp
        {
            return (Math.Sqrt((cont_y * cont_y) + (cont_z * cont_z)));
        }
    }
}
