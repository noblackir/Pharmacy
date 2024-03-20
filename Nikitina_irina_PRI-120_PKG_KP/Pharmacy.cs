using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Nikitina_irina_PRI_120_PKG_KP
{
    //Класс RGB для удобства задания цвета
    class RGB
    {
        private float R;
        private float G;
        private float B;

        public RGB(float R, float G, float B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public float getR()
        {
            return R;
        }

        public float getG()
        {
            return G;
        }

        public float getB()
        {
            return B;
        }
    }


    class Pharmacy
    {
        float deltaColor = 0;

        private void setColor(float R, float G, float B)
        {
            RGB color = new RGB(R - deltaColor, G - deltaColor, B - deltaColor);
            Gl.glColor3f(color.getR(), color.getG(), color.getB());
        }

        public void setDeltaColor(float delta)
        {
            deltaColor = delta;
        }

        //Отрисовка асфальта
        public void drawAsphalt()
        {


            Gl.glPushMatrix();
            setColor(0.22f, 0.19f, 0.22f);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(-150, 0, 0);
            Gl.glVertex3d(150, 0, 0);
            Gl.glVertex3d(150, 300, 0);
            Gl.glVertex3d(-150, 300, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
        }

        //Отрисовка фона
        public void drawBackground()
        {
            Gl.glPushMatrix();
            setColor(0, 0, 0);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(-150, 0, 0);
            Gl.glVertex3d(-150, 300, 0);
            Gl.glVertex3d(-150, 300, 300);
            Gl.glVertex3d(-150, 0, 300);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(-150, 300, 0);
            Gl.glVertex3d(150, 300, 0);
            Gl.glVertex3d(150, 300, 300);
            Gl.glVertex3d(-150, 300, 300);
            Gl.glEnd();

            Gl.glPopMatrix();
        }

        //Отрисовка фонаря
        public void drawFlashlight(bool isLight)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(30, 20, 22);

            Gl.glPushMatrix();
            Gl.glScaled(0.1f, 0.10f, 2.1f);
            setColor(0.1f, 0.1f, 0.2f);
            Glut.glutSolidCube(20);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(10, 0, 20);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glScaled(0.1f, 0.10f, 1f);
            setColor(0.1f, 0.1f, 0.2f);
            Glut.glutSolidCube(20);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(18, 0, 13);
            if (isLight)
                setColor(0.91f, 0.75f, 0.05f);
            else setColor(0, 0, 0);
            Glut.glutSolidCylinder(3, 6, 6, 5);
            setColor(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCylinder(3, 6, 6, 6);
            Gl.glPopMatrix();

            if (isLight)
            {
                Gl.glPushMatrix();
                Gl.glTranslated(12, 8, -21);
                drawCircle(15, 30, 0.51f, 0.45f, 0.05f);
                Gl.glPopMatrix();
            }

            Gl.glPopMatrix();
        }

        //Отрисовка аптеки
        public void drawPharmacy(uint sign)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(35, 95, 0);
            Gl.glRotated(-40, 0, 0, 1);

            Gl.glPushMatrix();
            drawSignboard(sign);
            Gl.glScaled(1.4, 1, 1);
            setColor(0.7f, 0.7f, 0.7f);
            Glut.glutSolidCylinder(40, 60, 7, 6);
            setColor(0, 0, 0);
            Gl.glLineWidth(6f);
            Glut.glutWireCylinder(40, 60, 7, 6);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(47, -25, 0);
            setColor(0.16f, 0.16f, 0.16f);
            Gl.glRotated(58, 0, 0, 1);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 35);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(15, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(15, 5, 35);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(47, -20, 17);
            setColor(0, 0, 0);
            Gl.glRotated(58, 0, 0, 1);

            Glut.glutSolidSphere(1, 12, 12);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireSphere(1, 12, 12);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Отрисовка вывески
        private void drawSignboard(uint sign)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(34, -28, 38);

            Gl.glRotated(58, 0, 0, 1);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glRotated(-180, 1, 0, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, sign);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 35);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(15, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(15, 5, 35);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();
        }

        //Отрисовка камня
        public void drawStone(double x, double z)
        {
            Gl.glPushMatrix();
            Gl.glRotated(90, 0, 0, 1);
            Gl.glTranslated(20, x - 65, 1 + z);
            setColor(0.08f, 0.08f, 0.08f);
            Gl.glScaled(1.2, 1, 1);
            Glut.glutSolidSphere(1.5, 12, 12);
            Gl.glPopMatrix();
        }

        //Приватный метод для отрисовки окружности
        private void drawCircle(float l, float cnt, float r, float g, float b)
        {
            Gl.glPushMatrix();

            float x, y;
            float a = (float)Math.PI * 2 / cnt;
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            setColor(r, g, b);
            Gl.glVertex2f(0, 0);

            for (int i = -1; i < cnt; i++)
            {
                x = (float)Math.Sin(a * i) * l;
                y = (float)Math.Cos(a * i) * l;
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();
            Gl.glPopMatrix();
        }

        //Отрисовка стены в аптеке
        public void drawWallOfPharmacy()
        {
            Gl.glPushMatrix();
            setColor(1, 1, 1);

            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(-50, 150, 0);
            Gl.glVertex3d(50, 150, 0);
            Gl.glVertex3d(50, 150, 100);
            Gl.glVertex3d(-50, 150, 100);
            Gl.glEnd();

            Gl.glPopMatrix();
        }

        //Отрисовка столешницы в аптеке
        public void drawTable()
        {
            Gl.glPushMatrix();
            Gl.glTranslated(0, 159, 7.5);
            Gl.glScaled(2f, 0.8f, 0.7f);
            setColor(0.8f, 0.8f, 0.8f);
            Glut.glutSolidCube(20);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(20);
            Gl.glPopMatrix();
        }

        //Отрисовка аптечки
        public void drawFirstAidKit(bool error)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(-13, 159, 20);
            Gl.glRotated(90, 0, 0, 1);
            Gl.glRotated(-45, 0, 0, 1);

            Gl.glPushMatrix();
            Gl.glTranslated(-5,0, 0.1);
            Gl.glScaled(0.1f, 1f, 0.7f);
            setColor(0.7f, 0.7f, 0.7f);
            Glut.glutSolidCube(10);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(0, 0, -4);
            Gl.glScaled(1f, 1f, 0.1f);
            setColor(0.7f, 0.7f, 0.7f);
            Glut.glutSolidCube(10);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            setColor(1, 0,0);
            Gl.glTranslated(-4.3, 1, -2);
            Gl.glRotated(-90, 0, 0, 1);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(1, 0, 0);
            Gl.glVertex3d(1, 0, 5);
            Gl.glVertex3d(0, 0, 5);
            Gl.glEnd();
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-5.3, -2, 0);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glTranslated(-4, 1, -2);
            Gl.glVertex3d(1, 0, 0);
            Gl.glVertex3d(1, 0, 1);
            Gl.glVertex3d(1, 5, 1);
            Gl.glVertex3d(1, 5, 0);
            Gl.glEnd();

            Gl.glPopMatrix();

            Gl.glPushMatrix();
            if (error) setColor(1, 0, 0);
            else setColor(0.23f, 0.84f, 0.43f);
            Gl.glTranslated(-4, 12, -5.3);
            Gl.glRotated(-45, 0, 0, 1);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(30, 0, -2);
            Gl.glVertex3d(30, 0, 13);
            Gl.glVertex3d(20, 0, 13);
            Gl.glVertex3d(20, 0, -2);
            Gl.glEnd();
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Отрисовка телевизора (Анимация)
        public void drawTV(double deltaZ, double deltaX)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(0, 151, 40);
            Gl.glPushMatrix();
            Gl.glScaled(1.8f, 0.1f, 1f);
            setColor(0,0,0);
            Glut.glutSolidCube(20);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(20);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            setColor(0.23f, 0.64f, 0.83f);
            Gl.glTranslated(-15, 3, -9);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(30, 0, 0);
            Gl.glVertex3d(30, 0, 17);
            Gl.glVertex3d(0, 0, 17);
            Gl.glVertex3d(0, 0, 0);
            Gl.glEnd();
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(0, 5, 4 + deltaZ);
            Gl.glRotated(90, 1, 0, 0);
            drawCircle(1.5f, 30, 1,0,0);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-10 + deltaX, 5, -1);
            Gl.glRotated(90, 1, 0, 0);
            drawCircle(1.5f, 30, 0, 1, 0);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Отрисовка сигарет
        public void drawSigarettes(uint texture, double deltaX)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(20 + deltaX, 153, 15.2);

            Gl.glPushMatrix();
            Gl.glTranslated(-5, 0, 0.1);
            Gl.glScaled(0.2f, 0.3f, 0.1f);
            setColor(1,0,0);
            Glut.glutSolidCube(9);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(9);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-4, 1.5, -4.33);
            Gl.glScaled(0.4f, 0.6f, 1f);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 5);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(4.5, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(4.5, 5, 5);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Отрисовка чипсов
        public void drawLays(uint texture, double deltaX)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(19+ deltaX, 158, 15.2);
            Gl.glRotated(90, 0, 0, 1);

            Gl.glPushMatrix();
            Gl.glTranslated(-1, 4, 0.1);
            Gl.glScaled(0.2f, 0.3f, 0.1f);
            setColor(1, 0, 0);
            Glut.glutSolidCube(13);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(13);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(0.4, 6, -4.1);
            Gl.glScaled(0.4f, 0.6f, 1f);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 7);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(7, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(7, 5, 7);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Отрисовка пластыря
        public void drawBandage(uint texture, double deltaX, bool flag)
        {
            Gl.glPushMatrix();
            if (!flag)
                Gl.glTranslated(20 + deltaX, 161, 15.2);
            else
            {
                Gl.glTranslated(-7, 155, 17);
                Gl.glRotated(-45, 0, 0, 1);
            }

            Gl.glPushMatrix();
            Gl.glTranslated(-5, 0, 0.1);
            Gl.glScaled(0.2f, 0.3f, 0.1f);
            setColor(0, 1, 0);
            Glut.glutSolidCube(10);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-4, 1.5, -4.2);
            Gl.glScaled(0.4f, 0.6f, 1f);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 5);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(5, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(5, 5, 5);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        public void drawActivatedCarbon(uint texture, double deltaX, bool flag)
        {
            Gl.glPushMatrix();
            if (!flag)
                Gl.glTranslated(20 + deltaX, 165, 15.2);
            else
            {
                Gl.glTranslated(-10, 158, 17);
                Gl.glRotated(-45, 0, 0, 1);
            }

            Gl.glPushMatrix();
            Gl.glTranslated(-5, 0, 0.1);
            Gl.glScaled(0.2f, 0.15f, 0.1f);
            setColor(0, 0, 0);
            Glut.glutSolidCube(10);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-4, 0.7, -4.25);
            Gl.glScaled(0.4f, 0.6f, 1f);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 5);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(2.5, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(2.5, 5, 5);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

    }
}

