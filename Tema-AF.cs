using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema_AF
{
    public partial class Form1 : Form
    {
        Bitmap sursa, destinatie;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sursa = new Bitmap(Image.FromFile(@"..\..\TEMA\Tema10.jpg"));
            pictureBox1.Image = sursa;
            destinatie = new Bitmap(sursa.Width, sursa.Height);
        }

        private void F1_Click(object sender, EventArgs e)
        {

            // Inițializăm o nouă imagine destinatar cu aceleași dimensiuni ca și imaginea sursă
            destinatie = new Bitmap(sursa.Width, sursa.Height);

            Color T;
            Color R;

            // Parcurgem fiecare pixel al imaginii sursă
            for (int i = 0; i < sursa.Width; i++)
            {
                for (int j = 0; j < sursa.Height; j++)
                {
                    // Obținem culoarea pixelului curent
                    T = sursa.GetPixel(i, j);

                    // Verificăm dacă pixelul este deja de culoare dominantă (gri)
                    if (T.R == T.G && T.R == T.B)
                    {
                        // Dacă este gri, se atribuie aceeași culoare
                        R = T;
                    }
                    else
                    {
                        // În caz contrar, determinăm culoarea dominantă și aplicăm filtrul
                        if (T.R > T.G && T.R > T.B)
                        {
                            R = Color.FromArgb(T.R, 0, 0); // Roșu dominant
                        }
                        else
                        {
                            if (T.B > T.G && T.B > T.R)
                            {
                                R = Color.FromArgb(0, 0, T.B); // Albastru dominant
                            }
                            else
                            {
                                // Dacă nu este o culoare dominantă, se menține culoarea pixelului original
                                R = T;
                                destinatie.SetPixel(i, j, R); // Aplicăm filtrul
                            }
                        }

                        // Actualizăm imaginea afișată în PictureBox
                        pictureBox2.Image = destinatie;
                    }
                }
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            sursa = new Bitmap(Image.FromFile(@"..\..\TEMA\Tema10.jpg"));
            pictureBox2.Image = sursa;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = sursa;
        }

        //Alb-Negru
        private void F1_Click_1(object sender, EventArgs e)
        {
            // Parcurgem fiecare pixel al imaginii sursă
            for (int i = 0; i < sursa.Width; i++)
            {
                for (int j = 0; j < sursa.Height; j++)
                {
                    // Obținem culoarea pixelului curent
                    Color source = sursa.GetPixel(i, j);

                    // Calculăm media aritmetică a culorilor (transformând imaginea în tonuri de gri)
                    int media = MediaAritmetica(source);

                    // Creăm o nouă culoare alb-negru bazată pe media calculată
                    Color dest = Color.FromArgb(media, media, media);

                    // Setăm pixelul în imaginea destinatarului cu noua culoare
                    destinatie.SetPixel(i, j, dest);
                }
            }

            // Afișăm imaginea destinatarului cu filtrul aplicat în PictureBox-ul pictureBox2
            pictureBox2.Image = destinatie;
        }

        // Această metodă calculează media aritmetică a culorilor dintr-o anumită culoare.
        private int MediaAritmetica(Color color)
        {
            // Media aritmetică este calculată ca suma componentelor R, G și B împărțită la 3.
            return (color.R + color.G + color.B) / 3;
        }

        //Contras
        private void F2_Click(object sender, EventArgs e)
        {
            // Parcurgem fiecare pixel al imaginii sursă
            for (int i = 0; i < sursa.Width; i++)
            {
                for (int j = 0; j < sursa.Height; j++)
                {
                    // Obținem culoarea pixelului de la coordonatele (i, j) de pe imaginea sursă
                    Color source = sursa.GetPixel(i, j);

                    // Calculăm media aritmetică a culorii pixelului curent
                    int media = MediaAritmetica(source);

                    // Inițializăm o culoare de destinație
                    Color dest;

                    // Verificăm dacă media culorii este sub pragul de 128 (jumătate din 255)
                    if (media < 128)
                    {
                        // Dacă media este sub prag, modificăm culoarea cu o anumită valoare negativă
                        dest = ChangeColorBy(source, -50);
                    }
                    else
                    {
                        // Dacă media este peste prag, modificăm culoarea cu o anumită valoare pozitivă
                        dest = ChangeColorBy(source, 50);
                    }

                    // Setăm culoarea pixelului corespunzător din imaginea destinatar
                    destinatie.SetPixel(i, j, dest);
                }
            }

            // Afișăm imaginea procesată în pictureBox2
            pictureBox2.Image = destinatie;
        }
        // Această metodă primește o culoare sursă și o valoare pentru a modifica intensitatea culorilor.
        private Color ChangeColorBy(Color source, int value)
        {
            // Se adaugă valoarea dată la componentele de culoare ale culorii sursă.
            int red = source.R + value;
            int green = source.G + value;
            int blue = source.B + value;

            // Se asigură că valorile sunt în intervalul [0, 255].
            if (red < 0) red = 0; // Dacă roșul este mai mic decât 0, este setat la 0.
            if (green < 0) green = 0; // Dacă verdele este mai mic decât 0, este setat la 0.
            if (blue < 0) blue = 0; // Dacă albastrul este mai mic decât 0, este setat la 0.

            if (red > 255) red = 255; // Dacă roșul este mai mare decât 255, este setat la 255.
            if (green > 255) green = 255; // Dacă verdele este mai mare decât 255, este setat la 255.
            if (blue > 255) blue = 255; // Dacă albastrul este mai mare decât 255, este setat la 255.

            // Se returnează o nouă culoare formată din componentele modificate.
            return Color.FromArgb(red, green, blue);
        }
        //Complementar
        private void F3_Click(object sender, EventArgs e)
        {// Parcurgem fiecare pixel al imaginii sursă
            for (int i = 0; i < sursa.Width; i++)
            {
                for (int j = 0; j < sursa.Height; j++)
                {
                    // Obținem culoarea pixelului de la coordonatele (i, j) de pe imaginea sursă
                    Color source = sursa.GetPixel(i, j);

                    // Calculăm inversul culorii pentru fiecare canal de culoare (roșu, verde, albastru)
                    int red = 255 - source.R;
                    int green = 255 - source.G;
                    int blue = 255 - source.B;

                    // Creăm o nouă culoare folosind inversul calculat pentru fiecare canal de culoare
                    Color dest = Color.FromArgb(red, green, blue);

                    // Setăm culoarea pixelului corespunzător din imaginea destinatar
                    destinatie.SetPixel(i, j, dest);
                }
            }

            // Afișăm imaginea procesată în pictureBox2
            pictureBox2.Image = destinatie;
        }
        //Blur
        private void F4_Click(object sender, EventArgs e)
        {
            // Inițializăm o nouă imagine destinatar cu aceleași dimensiuni ca și imaginea sursă
            destinatie = new Bitmap(sursa.Width, sursa.Height);

            // Variabila T va reprezenta culoarea pixelului de pe imaginea sursă
            Color T;

            // Parcurgem fiecare pixel al imaginii sursă
            for (int i = 0; i < sursa.Width; i++)
            {
                for (int j = 0; j < sursa.Height; j++)
                {
                    // Obținem culoarea pixelului de la coordonatele (i/10*10, j/10*10) de pe imaginea sursă
                    // Această operație împarte imaginea sursă în blocuri de 10x10 pixeli și preia culoarea din colțul stânga-sus al fiecărui bloc
                    T = sursa.GetPixel(i / 10 * 10, j / 10 * 10);

                    // Setăm culoarea pixelului corespunzător din imaginea destinatar
                    destinatie.SetPixel(i, j, T);
                }

                // Afișăm imaginea procesată în pictureBox2 la fiecare iterație a ciclului exterior (pentru a observa progresul pe parcurs)
                pictureBox2.Image = destinatie;
            }
        }
        //Filtru de sepia
        private void F5_Click(object sender, EventArgs e)
        {// Parcurgem fiecare pixel al imaginii sursă
            for (int i = 0; i < sursa.Width; i++)
            {
                for (int j = 0; j < sursa.Height; j++)
                {
                    // Obținem culoarea pixelului curent
                    Color source = sursa.GetPixel(i, j);

                    // Calculăm noile componente de culoare folosind transformarea sepia
                    int tr = Math.Min(255, (int)(0.393 * source.R + 0.769 * source.G + 0.189 * source.B));
                    int tg = Math.Min(255, (int)(0.349 * source.R + 0.686 * source.G + 0.168 * source.B));
                    int tb = Math.Min(255, (int)(0.272 * source.R + 0.534 * source.G + 0.131 * source.B));

                    // Creăm culoarea finală pe baza componentelor calculate
                    Color dest = Color.FromArgb(tr, tg, tb);

                    // Setăm pixelul din imaginea destinatar cu noua culoare
                    destinatie.SetPixel(i, j, dest);
                }
            }

            // Afișăm imaginea procesată în pictureBox2
            pictureBox2.Image = destinatie;
        }
        //Filtru Polaroid
        private void F6_Click(object sender, EventArgs e)
        {

            // Copiem imaginea originală
            Bitmap original = new Bitmap(sursa);

            // Adăugăm un cadru Polaroid
            using (Graphics g = Graphics.FromImage(original))
            {
                using (Pen pen = new Pen(Color.White, 20))
                {
                    g.DrawRectangle(pen, 0, 0, original.Width, original.Height);
                }
            }

            // Simulăm unele imperfecțiuni ale imaginii
            Random rnd = new Random();
            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    if (rnd.Next(100) < 5) // Adăugăm 5% zgomot aleatoriu
                    {
                        int noise = rnd.Next(256); // Generăm o valoare aleatoare de intensitate
                        Color pixel = original.GetPixel(x, y);
                        int r = Math.Min(255, Math.Max(0, pixel.R + noise));
                        int g = Math.Min(255, Math.Max(0, pixel.G + noise));
                        int b = Math.Min(255, Math.Max(0, pixel.B + noise));
                        original.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
            }

            // Simulăm o nuanță polaroid
            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixel = original.GetPixel(x, y);
                    int r = (int)(0.9 * pixel.R);
                    int g = (int)(0.9 * pixel.G);
                    int b = (int)(0.95 * pixel.B);
                    original.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            pictureBox2.Image = original;
        }
        //Detectarea marginilor
        private void F7_Click(object sender, EventArgs e)
        {// Definim matricile Sobel pentru detecția contururilor
            int[,] sobelX = { {-1, 0, 1},
                  {-2, 0, 2},
                  {-1, 0, 1} };

            int[,] sobelY = { {-1, -2, -1},
                  {0, 0, 0},
                  {1, 2, 1} };

            // Parcurgem fiecare pixel al imaginii, cu excepția marginilor
            for (int x = 1; x < sursa.Width - 1; x++)
            {
                for (int y = 1; y < sursa.Height - 1; y++)
                {
                    // Inițializăm sumele pentru derivatele parțiale
                    int sumX = 0, sumY = 0;

                    // Parcurgem vecinătatea fiecărui pixel pentru calculul derivatei parțiale
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            // Obținem culoarea pixelului din vecinătatea curentă
                            Color pixel = sursa.GetPixel(x + i, y + j);

                            // Convertim culoarea la tonuri de gri folosind formula standard
                            int grayValue = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);

                            // Calculăm derivatele parțiale folosind matricele Sobel
                            sumX += grayValue * sobelX[i + 1, j + 1];
                            sumY += grayValue * sobelY[i + 1, j + 1];
                        }
                    }

                    // Calculăm magnitudinea gradientului ca modulul vectorului (sumX, sumY)
                    int gradient = (int)Math.Sqrt(sumX * sumX + sumY * sumY);

                    // Limităm valorile la 255 pentru a evita supraîncărcarea
                    gradient = Math.Min(gradient, 255);

                    // Setăm culoarea pixelului rezultat în imaginea destinatar
                    destinatie.SetPixel(x, y, Color.FromArgb(gradient, gradient, gradient));
                }
            }

            // Afișăm imaginea rezultată în pictureBox2
            pictureBox2.Image = destinatie;
        }
        // Filtrul Pop Art
        private void F8_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < sursa.Width; x++)
            {
                for (int y = 0; y < sursa.Height; y++)
                {
                    Color pixel = sursa.GetPixel(x, y);

                    // Accentuăm culorile prin ajustarea componentelor RGB
                    int r = pixel.R > 180 ? 255 : 0;
                    int g = pixel.G > 180 ? 255 : 0;
                    int b = pixel.B > 180 ? 255 : 0;

                    // Creăm o nuanță de gri prin media aritmetică a componentelor
                    int gray = (pixel.R + pixel.G + pixel.B) / 3;

                    // Verificăm dacă suntem într-o zonă de contrast ridicat și aplicăm culori
                    if (Math.Abs(pixel.R - gray) > 80) r = 255;
                    if (Math.Abs(pixel.G - gray) > 80) g = 255;
                    if (Math.Abs(pixel.B - gray) > 80) b = 255;

                    destinatie.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            pictureBox2.Image = destinatie;
        }
        //Filtru cald
        private void F9_Click(object sender, EventArgs e)
        {
            double strength = 0.5; // Factorul de întărire pentru filtrul cald

            // Parcurgem fiecare pixel al imaginii sursă
            for (int x = 0; x < sursa.Width; x++)
            {
                for (int y = 0; y < sursa.Height; y++)
                {
                    // Obținem culoarea pixelului curent din imaginea sursă
                    Color pixel = sursa.GetPixel(x, y);

                    // Calculăm noile componente ale culorii pentru fiecare canal (roșu, verde, albastru)
                    // Întărim canalul roșu cu 50% și canalul verde cu 20%, păstrând canalul albastru nemodificat
                    int r = (int)(1.5 * pixel.R); // Întărim canalul roșu cu 50%
                    int g = (int)(1.2 * pixel.G); // Întărim canalul verde cu 20%
                    int b = pixel.B; // Păstrăm canalul albastru nemodificat

                    // Asigurăm că noile valori sunt în intervalul [0, 255]
                    r = Math.Min(255, Math.Max(0, r));
                    g = Math.Min(255, Math.Max(0, g));

                    // Setăm culoarea rezultată în imaginea destinatar
                    destinatie.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
                pictureBox2.Image = destinatie;
            }
        }
        //Cald in negru, iar reci,alb si negru in rosu
        private void Org1_Click(object sender, EventArgs e)
        {

            // Parcurgem fiecare pixel din imagine
            for (int x = 0; x < sursa.Width; x++)
            {
                for (int y = 0; y < sursa.Height; y++)
                {
                    Color pixel = sursa.GetPixel(x, y);

                    // Calculăm media culorilor pentru a determina dacă este o culoare caldă sau rece
                    int average = (pixel.R + pixel.G + pixel.B) / 3;

                    // Dacă culoarea este considerată caldă
                    if (pixel.R > pixel.B && pixel.G > pixel.B && pixel.R > 150)
                    {
                        // Setăm pixelul la negru
                        destinatie.SetPixel(x, y, Color.Black);
                    }
                    // Dacă culoarea este considerată rece
                    else
                    {
                        // Setăm pixelul la roșu
                        destinatie.SetPixel(x, y, Color.Red);
                    }
                }
            }

            // Actualizăm imaginea afișată
            pictureBox2.Image = destinatie;

        }
    
        //impartirea in 4 si colorare diferita
        private void Org2_Click(object sender, EventArgs e)
        {
            // Copiem imaginea originală pentru a nu afecta imaginea originală
            Bitmap original = new Bitmap(sursa);

            // Calculăm dimensiunile fiecărui sfert
            int width = original.Width / 2;
            int height = original.Height / 2;

            // Parcurgem fiecare sfert și aplicăm culoarea corespunzătoare
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    // Creeăm un nou bitmap pentru sfertul respectiv
                    Bitmap quadrant = new Bitmap(width, height);

                    // Determinăm culoarea pentru sfertul curent
                    Color color;
                    switch (x + y * 2)
                    {
                        case 0: // Primul sfert - albastru
                            color = Color.Blue;
                            break;
                        case 1: // Al doilea sfert - verde
                            color = Color.Green;
                            break;
                        case 2: // Al treilea sfert - roșu
                            color = Color.Red;
                            break;
                        case 3: // Al patrulea sfert - galben
                            color = Color.Yellow;
                            break;
                        default:
                            color = Color.Black; // Culoare implicită pentru orice alt sfert
                            break;
                    }

                    // Colorăm sfertul cu culoarea corespunzătoare
                    using (Graphics g = Graphics.FromImage(quadrant))
                    {
                        using (SolidBrush brush = new SolidBrush(color))
                        {
                            g.FillRectangle(brush, 0, 0, width, height);
                        }
                    }

                    // Aplicăm transparența pentru a vedea imaginea originală sub sfert
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            Color originalPixel = original.GetPixel(x * width + i, y * height + j);
                            Color quadrantPixel = quadrant.GetPixel(i, j);

                            // Calculăm culoarea rezultată folosind o medie ponderată între culoarea sfertului și imaginea originală
                            int alpha = 128; // Valoarea transparenței (0 - complet transparent, 255 - complet opac)
                            int red = (quadrantPixel.R * alpha + originalPixel.R * (255 - alpha)) / 255;
                            int green = (quadrantPixel.G * alpha + originalPixel.G * (255 - alpha)) / 255;
                            int blue = (quadrantPixel.B * alpha + originalPixel.B * (255 - alpha)) / 255;

                            // Setăm culoarea rezultată pentru pixelul din sfertul respectiv
                            quadrant.SetPixel(i, j, Color.FromArgb(red, green, blue));
                        }
                    }

                    // Suprapunem sfertul colorat peste imaginea originală
                    using (Graphics g = Graphics.FromImage(original))
                    {
                        g.DrawImage(quadrant, new Point(x * width, y * height));
                    }
                }
            }

            // Afișăm imaginea procesată
            pictureBox2.Image = original;
        }
        //Filtru rece
        private void F10_Click(object sender, EventArgs e)
        {   // Parcurgem fiecare pixel al imaginii sursă
            for (int x = 0; x < sursa.Width; x++)
            {
                for (int y = 0; y < sursa.Height; y++)
                {
                    // Obținem culoarea pixelului curent din imaginea sursă
                    Color pixel = sursa.GetPixel(x, y);

                    // Calculăm noile componente ale culorii pentru fiecare canal (roșu, verde, albastru)
                    // Reducem intensitatea canalului roșu și verde cu 30% și păstrăm canalul albastru nemodificat
                    int r = (int)(0.7 * pixel.R);
                    int g = (int)(0.7 * pixel.G);
                    int b = pixel.B;

                    // Asigurăm că noile valori sunt în intervalul [0, 255]
                    r = Math.Min(255, Math.Max(0, r));
                    g = Math.Min(255, Math.Max(0, g));

                    // Setăm culoarea rezultată în imaginea destinatar
                    destinatie.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
                pictureBox2.Image = destinatie;
            }
        }
    }
}
