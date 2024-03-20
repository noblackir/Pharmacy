using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.DevIl;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Nikitina_irina_PRI_120_PKG_KP
{
    public partial class Form1 : Form
    {
        double angle = 3, angleX = -96, angleY = 0, angleZ = -30;
        double sizeX = 1, sizeY = 1, sizeZ = 1;

        double translateX = -9, translateY = -60, translateZ = -10;

        double cameraSpeed;
        float global_time = 0;

        //Флаги, отражающие использование камня и наличие света
        bool isLight = true;
        bool isStoneUsed = false;
        bool isStoneUsing = false;
        //Флаги, показывающие нахождение в аптечке пластыря и угля
        bool isBandage, isActivatedCarbon,isError = false;
        //Флаги, отражающие направления мячиков
        bool isRightBall, isUpBall = false;


        //Дельта перемещения камня
        double deltaXStone, deltaZStone = 0;
        //Дельта перемещения мячиков в телевизоре
        double deltaXBall, deltaZBall;
        //Дельта перемещения объектов на столе в аптеке
        double deltaSigarettes, deltaLays, deltaBandage, deltaActivatedCarbon;

        //Текстуры
        uint signboardSign, sigarettesSign, laysSign, activatedCarbonSign, bandageSign;
        int imageId;
        string signboardTexture = "signboard.png";
        string sigarettesTexture = "sigarettes.png";
        string laysTexture = "lays.png";
        string activatedCarbonTexture = "activated_carbon.png";
        string bandageTexture = "bandage.png";

        //Взрыв фонаря с использованием системы частиц
        private readonly Explosion explosion = new Explosion(74, 15, 30, 30, 50);

        //Проигрывание аудио
        public WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();

        private void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glLoadIdentity();
            Gl.glPushMatrix();
            Gl.glRotated(angleX, 1, 0, 0);
            Gl.glRotated(angleY, 0, 1, 0);
            Gl.glRotated(angleZ, 0, 0, 1);
            Gl.glTranslated(translateX, translateY, translateZ);
            Gl.glScaled(sizeX, sizeY, sizeZ);
            explosion.Calculate(global_time);
            pharmacy.drawAsphalt();
            pharmacy.drawBackground();
            pharmacy.drawFlashlight(isLight);
            pharmacy.drawPharmacy(signboardSign);
            pharmacy.drawWallOfPharmacy();
            pharmacy.drawTable();
            pharmacy.drawFirstAidKit(isError);
            pharmacy.drawTV(deltaZBall, deltaXBall);
            drawObjectsOnTheTable();
            drawFractal();
            if (!isStoneUsed) pharmacy.drawStone(deltaXStone, deltaZStone);

            if (isStoneUsing)
            {
                if (deltaXStone > 12)
                {
                    deltaXStone = 0;
                    deltaZStone = 0;
                    isStoneUsed = true;
                    isStoneUsing = false;
                    isLight = false;
                    pharmacy.setDeltaColor(0.1f);
                    Gl.glPushMatrix();
                    explosion.SetNewPosition(82, 15, 30);
                    explosion.SetNewPower(5);
                    explosion.Boooom(global_time);
                    Gl.glPopMatrix();
                    interfaceOneInit();
                }
                else
                {
                    deltaXStone += 3;
                    deltaZStone += 7;
                }
            }

            checkDeltaOfTheBall();


            Gl.glPopMatrix();
            Gl.glFlush();
            AnT.Invalidate();
        }

        //Метод для отрисовки объектов на столе
        private void drawObjectsOnTheTable()
        {
            pharmacy.drawSigarettes(sigarettesSign, deltaSigarettes);
            pharmacy.drawLays(laysSign, deltaLays);
            pharmacy.drawBandage(bandageSign, deltaBandage, isBandage);
            pharmacy.drawActivatedCarbon(activatedCarbonSign, deltaActivatedCarbon, isActivatedCarbon);

        }

        //Метод для создания видимости перемещения мячей в телевизоре
        private void checkDeltaOfTheBall()
        {
            if (deltaXBall == 20 || deltaXBall == 0)
            {
                if (isRightBall)
                {
                    isRightBall = false;
                    deltaXBall = 1;
                }
                else
                {
                    isRightBall = true;
                    deltaXBall = 19;
                }
            }
            else
            if (isRightBall) deltaXBall -= 1; else deltaXBall += 1;

            if (deltaZBall == -10 || deltaZBall == 0)
            {
                if (isUpBall)
                {
                    isUpBall = false;
                    deltaZBall = -1;
                }
                else
                {
                    isUpBall = true;
                    deltaZBall = -9;
                }
            }
            else
           if (isUpBall) deltaZBall += 1; else deltaZBall -= 1;
        }

        private void breakFlashlight()
        {
            isStoneUsing = true;
            interfaceOneInit();
        }

        private void fixFlashlight()
        {
            pharmacy.setDeltaColor(0);
            isLight = true;
            interfaceOneInit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                angle = 3; angleX = -90; angleY = 0; angleZ = -40;
                sizeX = 1; sizeY = 1; sizeZ = 1;
                translateX = -100; translateY = 10; translateZ = -25;
                interfaceOneInit();
                WMP.controls.stop();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                translateX = 0; translateY = -215; translateZ = -28;
                angleX = -90;
                angleZ = 180;
                interfaceTwoInit();
                WMP.controls.stop();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                translateX = 0; translateY = -167; translateZ = -43;
                angleX = -15;
                angleZ = 180;
                interfaceThreeInit();
                WMP.URL = @"kassa.mp3";
                WMP.controls.play();
            }
            if (comboBox1.SelectedIndex == 3)
            {
                translateX = -15; translateY = -159; translateZ = -30;
                angleX = 0;
                angleZ = 180;
                interfaceFourInit();
                WMP.controls.stop();
            }
            AnT.Focus();
        }

        private void interfaceOneInit()
        {
            if (isLight)
            {
                if (isStoneUsed)
                {
                    button1.Visible = false;
                    label7.Text = "Камень я не дам";
                }
                else
                {
                    button1.Visible = true;
                    button1.Text = "Разбить фонарь";
                    label7.Text = "Административнное нарушение";
                }
            }
            else
            {
                button1.Visible = true;
                button1.Text = "Починить фонарь";
                label7.Text = "Да будет свет";
            }
            label8.Visible = false;
            label9.Visible = false;
        }

        private void interfaceTwoInit()
        {
            button1.Visible = false;
            label8.Visible = false;
            label7.Visible = true;
            label9.Visible = true;
            label7.Text = "Тут есть телевизор";
            label9.Text = "А какой фрактал красивый...";
        }

        private void interfaceThreeInit()
        {
            if (!isError)
            {
                if (isBandage && isActivatedCarbon)
                {
                    button1.Visible = true;
                    button1.Text = "Ещё разок?";
                    label7.Text = "Победа!";
                } else if (isActivatedCarbon || isBandage)
                {
                    button1.Visible = false;
                    label7.Text = "Почти собрана!";
                } else
                {
                    button1.Visible = false;
                    label7.Text = "Необходимо собрать аптечку";
                }
            }
            else
            {
                button1.Visible = true;
                button1.Text = "Ещё разок?";
                label7.Text = "Странная аптечка получилась";
            }
            label8.Visible = true;
            label9.Visible = true;
            label8.Text = "1,2,3,4 - сдвиг товаров вправо";
            label9.Text = "Передвигай лекарства в выделенную область";
        }

        private void interfaceFourInit()
        {
            button1.Visible = false;
            label8.Visible = false;
            label7.Visible = true;
            label9.Visible = true;
            label7.Text = "Интересно, что же тут лишнее...";
            label9.Text = "Собери в аптечку только лекарства";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnT.Focus();
        }

        private void AnT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                translateY -= cameraSpeed;

            }
            if (e.KeyCode == Keys.S)
            {
                translateY += cameraSpeed;
            }
            if (e.KeyCode == Keys.A)
            {
                translateX += cameraSpeed;
            }
            if (e.KeyCode == Keys.D)
            {
                translateX -= cameraSpeed;

            }
            if (e.KeyCode == Keys.ControlKey)
            {
                translateZ += cameraSpeed;

            }
            if (e.KeyCode == Keys.Space)
            {
                translateZ -= cameraSpeed;
            }
            if (e.KeyCode == Keys.R)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        angleX += angle;

                        break;
                    case 1:
                        angleY += angle;

                        break;
                    case 2:
                        angleZ += angle;

                        break;
                    default:
                        break;
                }
            }
            if (e.KeyCode == Keys.E)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        angleX -= angle;
                        break;
                    case 1:
                        angleY -= angle;
                        break;
                    case 2:
                        angleZ -= angle;
                        break;
                    default:
                        break;
                }
            }
            if (e.KeyCode == Keys.D1)
            {
                if (!isError || (!(isBandage && isActivatedCarbon) && !isError))
                {
                    deltaSigarettes -= 1;
                    checkObjectsOnTheTable();
                }
                
            }
            if (e.KeyCode == Keys.D2)
            {
                if (!isError || (!(isBandage && isActivatedCarbon) && !isError))
                {
                    deltaLays -= 1;
                    checkObjectsOnTheTable();
                }
            }
            if (e.KeyCode == Keys.D3)
            {
                if (!isError || (!(isBandage && isActivatedCarbon) && !isError))
                {
                    deltaBandage -= 1;
                    checkObjectsOnTheTable();
                }
            }
            if (e.KeyCode == Keys.D4)
            {
                if (!isError || (!(isBandage && isActivatedCarbon) && !isError))
                {
                    deltaActivatedCarbon -= 1;
                    checkObjectsOnTheTable();
                }
            }

        }

        private void checkObjectsOnTheTable()
        {
            if (deltaSigarettes <= -10 || deltaLays <= -10)
            {
                isError = true;
                deltaLays = 0;
                deltaSigarettes = deltaBandage = deltaActivatedCarbon = 0;
            }
            else
            {
                if (deltaBandage <= -10)
                {
                    isBandage = true;
                    deltaSigarettes = deltaBandage = deltaActivatedCarbon = deltaLays = 0;
                } else if (deltaActivatedCarbon <= -10)
                {
                    isActivatedCarbon = true;
                    deltaSigarettes = deltaBandage = deltaActivatedCarbon = deltaLays = 0;
                }
            }
            if (isBandage && isActivatedCarbon) MessageBox.Show("Ура!!");
            interfaceThreeInit();


        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            global_time += (float)RenderTimer.Interval / 1000;
            Draw();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0)
                cameraSpeed = (double)numericUpDown1.Value;
            AnT.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    if (isLight) breakFlashlight();
                    else fixFlashlight();
                    break;
                case 2:
                    isError = false;
                    isBandage = false;
                    isActivatedCarbon = false;
                    interfaceThreeInit();
                    break;
            }
        }

        Pharmacy pharmacy = new Pharmacy();

       

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        public void drawFractal()
        {
            Gl.glPushMatrix();

            Gl.glTranslated(0, 167.5, 4);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glRotated(90, 0, 0, 1);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glScalef(1, 0.65f, 0.5f);
            
            Gl.glBegin(Gl.GL_LINES);
            drawLevyFractal(-8, 0, 8, 0, 16);
            Gl.glEnd();
            Gl.glPopMatrix();
        }


        void drawLevyFractal(int x1, int y1, int x2, int y2, int i)
        {
            if (i == 0)
            {
                Gl.glColor3f(1, 0, 0);
                Gl.glVertex2i(x1, y1); //координаты вырисовываемого 
                Gl.glVertex2i(x2, y2); //отрезка
            }
            else
            {
                int x3 = (x1 + x2) / 2 - (y2 - y1) / 2; //координаты
                int y3 = (y1 + y2) / 2 + (x2 - x1) / 2; //точки излома
                drawLevyFractal(x1, y1, x3, y3, i - 1);
                drawLevyFractal(x3, y3, x2, y2, i - 1);
            }
        }


        private void информацияОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация openGL (glut)
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            // цвет очистки окна
            Gl.glClearColor(255, 255, 255, 1);

            // настройка порта просмотра
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(60, (float)AnT.Width / (float)AnT.Height, 0.1, 900);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            cameraSpeed = 5;

            signboardSign = genImage(signboardTexture);
            sigarettesSign = genImage(sigarettesTexture);
            laysSign = genImage(laysTexture);
            bandageSign = genImage(bandageTexture);
            activatedCarbonSign = genImage(activatedCarbonTexture);

            RenderTimer.Start();
        }

        private uint genImage(string image)
        {
            uint sign = 0;
            Il.ilGenImages(1, out imageId);
            Il.ilBindImage(imageId);
            if (Il.ilLoadImage(image))
            {
                int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);
                switch (bitspp)
                {
                    case 24:
                        sign = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                        break;
                    case 32:
                        sign = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                        break;
                }
            }
            Il.ilDeleteImages(1, ref imageId);
            return sign;
        }

        private static uint MakeGlTexture(int Format, IntPtr pixels, int w, int h)
        {
            uint texObject;
            Gl.glGenTextures(1, out texObject);
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);
            switch (Format)
            {

                case Gl.GL_RGB:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

                case Gl.GL_RGBA:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

            }
            return texObject;
        }
    }
}
