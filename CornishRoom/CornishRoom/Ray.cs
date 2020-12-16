using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTrace {
    public class Ray
    {
        public PointXYZ Start { get; set; }
        public PointXYZ End { get; set; }

        public Ray(PointXYZ st, PointXYZ end)
        {
            Start = new PointXYZ(st);
            End = PointXYZ.normal(end - st);
        }

        public Ray() { }

        public Ray(Ray r)
        {
            Start = r.Start;
            End = r.End;
        }

        //отражение
        /*
         Направление отраженного луча определяется по закону:
         отраженный луч = падающий луч -  2* нормаль к точке попадания луча на сторону  на скалярное произведение падающего луча и нормали
         из презентации
             */
        public Ray Reflect(PointXYZ hit_point, PointXYZ normal)
        {
            //высчитываем направление отраженного луча
            PointXYZ reflect_dir = End - 2 * normal * PointXYZ.ScalarMul(End, normal);
            return new Ray(hit_point, hit_point + reflect_dir);
        }

        //преломление
        //все вычисления взяты из презентации
        public Ray Refract(PointXYZ hit_point, PointXYZ normal,float refraction ,float refract_coef)
        {
            Ray res_ray = new Ray();
            float sclr = PointXYZ.ScalarMul(normal, End);
            /*
             Если луч падает,то он проходит прямо,не преломляясь
             */
            float n1n2div = refraction / refract_coef;
            float theta_formula = 1 - n1n2div*n1n2div * (1 - sclr * sclr);
            if (theta_formula >= 0)
            {
                float cos_theta = (float)Math.Sqrt(theta_formula);
                res_ray.Start = new PointXYZ(hit_point);
                res_ray.End = PointXYZ.normal(End * n1n2div - (cos_theta + n1n2div * sclr) * normal);
                return res_ray;
            }
            else
                return null;
        }
    }

}
