using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Web.Script.Serialization;



#region A faire
// A faire:
/*
 * - le son quand on fait ctrl+m vient du fait qu'on ne renvoie pas true dans ProcessCmdKey
 * - Déplacer l'affichage de la réponse dans le thread parallèle
 * - Créer un thread pour les questions (il attend que la réponse soit donnée; il passe à la question suivante si oui ou si le temps est écoulé => Timer ? De plus, il affiche la réponse 1 seconde après qu'elle soit tapée - de couleur verte si la réponse est exacte et de couleur rouge si la réponse est fausse).
 * - Les raccourcis double. ex: ctrl+k, ctrl+d
 * - La gestion des questions (thread ?)
 * - La barre de progression
 * - Le fichier de paramètres (où on lit les raccourcis - penser aux sections (ex: edition, fenêtre, sélection)
 * - La gestion du temps de réponse
 * - Voir pourquoi ProcessCmdKey ne reçoit pas les evt KeyUp et KeyPress
 * - Faire une note de ce que je dois approfondir
 * 
 * 
*/
#endregion

#region A approfondir
/*
 * - Les Timers
 * - La gestion du clavier (notamment ProcessCmdKey)
 * - Créer un "Control" personnalisé (comme vu avec la classe RevealLabel)
 * - 
 * 
 * 
 * 
*/
#endregion





/**
 * Nom         : PlusUltra (plus loin).
 * Description : Permet de s'entraîner de manière progressive et complète au maniement du clavier.
 *               il s'agit d'un jeu de questions/réponses. Le programme pose une question et l'utilisateur
 *               doit donner la bonne réponse.
 *               Les questions sont issues d'un fichier de paramètres modifiable par l'utilisateur
 *               Il y a plusieurs cas possibles :
 *                  - la réponse est correcte :
 *                      - il y a progression
 *                      - on décrémente le compteur pour la question (les questions ont un compteur pour définir combien de fois elles seront posées)
 *                  - la réponse est mauvaise :
 *                      - il n'y a pas de progression
 *                      -
 *                 - Mode Raccourcis : Pour mémoriser les raccourcis clavier des applications
 *                 - Mode Commandes  : Pour mémoriser les commandes (shell, batch, etc ...)
 *                 - Mode dictée     : Pour s'entraîner à taper au clavier
 *                 - Mode Image      : Pour mémoriser des mouvements (Rukik's Cube par exemple)
 * Remarques : De manière plus générale, il s'agit d'un jeu de questions/réponses
 */


namespace PlusUltra
{
    public partial class wfPlusUltra : Form
    {
        private System.Windows.Forms.Timer reponseTimer = new System.Windows.Forms.Timer();
        private int compteTimer;
        private int compteQuestion;
        private bool tropLong;
        private string raccourciTrouve;
        //private bool control_K_Actif = false;
        //private bool prefixSeen;
        private Dictionary<string, string> listeRaccourcisQuestions = new Dictionary<string, string>();
        private Dictionary<string, string> listeQuestionsRaccourcis = new Dictionary<string, string>();
        private Dictionary<Keys, string> listeQuestionsReponses = new Dictionary<Keys, string>();

        public wfPlusUltra()
        {
            InitializeComponent();


            listeRaccourcisQuestions.Add("ctrl+X", "Cut line.");
            listeRaccourcisQuestions.Add("ctrl+enter", "Insert line after.");
            listeRaccourcisQuestions.Add("shift+ctrl+enter", "Insert line before.");
            listeRaccourcisQuestions.Add("ctrl+L", "Select line - Repeat to select next lines.");
            listeRaccourcisQuestions.Add("ctrl+D", "Select word - repeat select other occurrences.");
            listeRaccourcisQuestions.Add("ctrl+M", "Go to matching parentheses");
            listeRaccourcisQuestions.Add("shift+ctrl+M", "Select all contents of the current parentheses");

            foreach (KeyValuePair<string, string> kvp in listeRaccourcisQuestions)
            {
                Console.WriteLine("Key = {0}, Value = {1}",
                    kvp.Key, kvp.Value);
            }

            foreach (KeyValuePair<string, string> kvp in listeRaccourcisQuestions)
            {
                listeQuestionsReponses.Add(raccourciContient(kvp.Key), kvp.Key);
            }

            foreach (KeyValuePair<string, string> kvp in listeRaccourcisQuestions)
            {
                listeQuestionsRaccourcis.Add(kvp.Value, kvp.Key);
            }

            reponseTimer.Interval = 500;
            reponseTimer.Tick += new EventHandler(reponseTimer_Tick);
            compteTimer = 0;
            compteQuestion = 0;
            tropLong = false;
            raccourciTrouve = "";


            //RootObject ro = new RootObject();
            //try
            //{
            //    StreamReader sr = new StreamReader(FileLoc);
            //    string jsonString = sr.ReadToEnd();
            //    JavaScriptSerializer ser = new JavaScriptSerializer();
            //    ro = ser.Deserialize<RootObject>(jsonString);
            //}

        }

        void reponseTimer_Tick(object sender, EventArgs e)
        {
            compteTimer +=1;

            if (compteTimer == 1)                   // Le timer a été réinitialisé car :
            {
                if (tropLong == true)               // le temps de réponse est dépassé
	            {
                    Console.WriteLine("Réinitialisation de la variable tropLong.");
                    tropLong = false;               // Pour laisser le temps à l'utilisateur de comprendre ce qu'il se passe
                                                    // On n'efface pas l'écran de suite
	            }
                else if (raccourciTrouve != "")    // L'utilisateur a écrit un raccourci connu
                {
                    // Est-ce que ce raccourci correspond à la question posée ?
                    //Console.WriteLine("listeRaccourcisQuestions[listeRaccourcisQuestions.Keys.ToList()[compteQuestion]] : {0}.", listeRaccourcisQuestions[listeRaccourcisQuestions.Keys.ToList()[compteQuestion]]);
                    //Console.WriteLine("listeRaccourcisQuestions[raccourciTrouve] : {0}.", listeRaccourcisQuestions[raccourciTrouve]);
                    //if (listeRaccourcisQuestions[raccourciTrouve] == listeRaccourcisQuestions[listeRaccourcisQuestions.Keys.ToList()[compteQuestion-1]])
                    if (listeRaccourcisQuestions[raccourciTrouve] == lblQuestion.Text)
                    // ou == lblQuestion.Text ?
                    {
                        Console.WriteLine("Raccourci exact : {0}.", raccourciTrouve);
                        // 1. On efface le raccourci de la text box
                        tbReponse.Text = "";
                        // 2. On signale à l'utilisateur que la réponse est juste (en vert)
                        lblReponse.Text = raccourciTrouve + " : " + listeRaccourcisQuestions[raccourciTrouve];
                        lblReponse.ForeColor = System.Drawing.Color.Green;
                    }
                    else // le raccourci est faux
                    {
                        Console.WriteLine("Raccourci faux : {0}.", raccourciTrouve);
                        // 1. on le supprime de la textbox
                        tbReponse.Text = "";
                        // 2. On signale à l'utilisateur que la réponse est fausse (en rouge)
                        lblReponse.Text = raccourciTrouve + " : FAUX !";
                        lblReponse.ForeColor = System.Drawing.Color.Red;
                    }
                    // On réinitialise le raccourci trouvé
                    raccourciTrouve = "";
                    // On réinitialise le compteur
                    compteTimer = 0;
                    
                }
                else if (compteQuestion < 7)  // La bonne réponse a été donnée, on affiche une nouvelle question
                {
                    // 1. On efface le champ de réponse
                    lblReponse.Text = "";
                    // 2. On affiche la nouvelle question
                    lblQuestion.Text = listeRaccourcisQuestions[listeRaccourcisQuestions.Keys.ToList()[compteQuestion]];
                    // 3. On compte le nombre de questions
                    compteQuestion += 1;
                    Console.WriteLine("Nouvelle question : {0}.", lblQuestion.Text);
                }  
            }
            

            if (compteTimer > 10)        // Les 5 secondes d'attente de la réponse sont dépassées
            {
                Console.WriteLine("5 secondes d'attente dépassées !");
                // 1. On mémorise que le temps d'attente est dépassé (pour la prochaine entrée dans la fonction)
                tropLong = true;
                // 2. On signale à l'utilisateur que le temps est écoulé
                lblReponse.Text = lblReponse.Text + " - Trop long !";
                lblReponse.ForeColor = System.Drawing.Color.Red;
                // On réinitialise le compteur
                compteTimer = 0;
            }
            else if (compteTimer > 0)                      // Le compteur tourne
            {
                Console.WriteLine("Plus que {0} secondes.", (5000 - (compteTimer*reponseTimer.Interval))/1000.0);
            }

            if (compteQuestion > 6) // On a fait le tour des questions, on revient à la première
            {
                Console.WriteLine("Redémarrage des questions.");
                compteQuestion = 0;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //const int WM_KEYDOWN = 0x100;

            //if (msg.Msg == WM_KEYDOWN)
            //{
                //Console.WriteLine("Message reçu : {0}.", msg.Msg);
                string value = "";
                if (listeQuestionsReponses.TryGetValue(keyData, out value))
                {
                   // Console.WriteLine("Raccourci identifié : {0}.", value);
                    tbReponse.Text = value;
                    raccourciTrouve = value;
                    // Que la réponse soit bonne ou pas, l'utilisateur a répondu
                    compteTimer = 0;
                    //return true;
                }
                else
                {
                    if (keyData == (Keys.Menu | Keys.Alt)
                     || keyData == (Keys.ControlKey | Keys.Control)
                     || keyData == (Keys.ShiftKey | Keys.Shift)

                     || keyData == (Keys.ControlKey | Keys.Control | Keys.Alt)
                     || keyData == (Keys.ControlKey | Keys.Control | Keys.Shift)

                     || keyData == (Keys.Menu | Keys.Alt | Keys.Control)
                     || keyData == (Keys.Menu | Keys.Alt | Keys.Shift)

                     || keyData == (Keys.ShiftKey | Keys.Shift | Keys.Alt)
                     || keyData == (Keys.ShiftKey | Keys.Shift | Keys.Control)

                     || keyData == (Keys.ShiftKey | Keys.Alt | Keys.Control | Keys.Shift)
                     || keyData == (Keys.Menu | Keys.Alt | Keys.Shift | Keys.Control)
                     || keyData == (Keys.ControlKey | Keys.Alt | Keys.Control | Keys.Shift)

                     || keyData == Keys.CapsLock)
                    {
                        //Console.WriteLine("Raccourci non terminé : {0}.", keyData.ToString());
                    }
                    else
                    {

                        Console.WriteLine("Raccourci inconnu : {0}.", format_Input(keyData.ToString()));
                        //lblReponse.Text = format_Input(keyData.ToString());
                        //lblReponse.ForeColor = System.Drawing.Color.Red;

                        // Que la réponse soit bonne ou pas, l'utilisateur a répondu
                        compteTimer = 0;
                    }
                }
            //}
            //else
            //{
            //    Console.WriteLine("Autre message reçu : {0}.", msg.Msg);
            //}


            #region A virer
            /*
            if (prefixSeen)
            {
                if (keyData == (Keys.Alt | Keys.Control | Keys.P))
                {
                    MessageBox.Show("Got it!");
                }
                prefixSeen = false;
                return true;
            }
            if (keyData == (Keys.Alt | Keys.Control | Keys.K))
            {
                prefixSeen = true;
                return true;
            }
            
            
            if (keyData == (Keys.Control | Keys.F))
            {
                tbReponse.Text = "ctrl+f";
                control_K_Actif = false;
                return true;
            }
            if (keyData == (Keys.Control | Keys.Shift | Keys.F))
            {
                tbReponse.Text = "ctrl+shift+f";
                control_K_Actif = false;
                return true;
            }
            if (keyData == (Keys.Control | Keys.K))
            {
                tbReponse.Text = "ctrl+k";
                control_K_Actif = true ;
                return true;
            }

            if (keyData == (Keys.Control | Keys.C))
            {
                //MessageBox.Show("What the Ctrl+F?");
                tbReponse.Text = "ctrl+c";
                if (control_K_Actif == true)
                {
                    tbReponse.Text = "ctrl+k+ctrl+c : mise en commentaire";
                }
                
                return true;
            }
            if (keyData == (Keys.Control | Keys.Alt | Keys.C))
            {
                //MessageBox.Show("What the ctrl+alt+c?");
                tbReponse.Text = "ctrl+alt+c" ;
                control_K_Actif = false;
                return true;
            }

            control_K_Actif = false;*/
            #endregion


            return base.ProcessCmdKey(ref msg, keyData);
        }     
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /*if (Control.ModifierKeys == Keys.Shift)
            {
               //MessageBox.Show("Couleur de l'étiquette : " + VoyantMajuscule.ForeColor.ToString() );
               //VoyantMajuscule.ForeColor = System.Drawing.Color.Red; 
            }
            else if (Control.ModifierKeys == Keys.Control)
            {
                
            }
            else if (Control.ModifierKeys == Keys.Alt)
            {

            }*/
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //Console.WriteLine("--->");
            //Console.WriteLine("Key value : {0}", e.KeyValue);
            //If shift key was pressed, it's not a number.
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                VoyantMajuscule.ForeColor = System.Drawing.Color.Red ;
                //Console.WriteLine("Touche SHIFT pressée.");
            }
            //If shift key was pressed, it's not a number.
            if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                VoyantAlt.ForeColor = System.Drawing.Color.Red;
                //Console.WriteLine("Touche ALT pressée.");
            }
            //If shift key was pressed, it's not a number.
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                VoyantControl.ForeColor = System.Drawing.Color.Red;
                //Console.WriteLine("Touche CONTROL pressée.");
            }

            if ((Control.ModifierKeys & Keys.CapsLock) == Keys.CapsLock)
            {
                VoyantMajuscule.ForeColor = System.Drawing.Color.Red;
                //Console.WriteLine("Touche Verr. MAJ. pressée.");
            }

            //Console.WriteLine("Touche {0} pressée.", e.KeyData);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //Console.WriteLine("Key value : {0}", e.KeyValue);
            //If shift key was pressed, it's not a number.
            //if ((e.KeyData & Keys.ShiftKey) == Keys.ShiftKey)
            if ((e.KeyValue) == 16)
            {
                VoyantMajuscule.ForeColor = System.Drawing.Color.Gray;
                //Console.WriteLine("Touche SHIFT relâchée.");
            }
            //If shift key was pressed, it's not a number.
            //if ((e.KeyData & Keys.Alt) == Keys.Alt)
            if ((e.KeyValue) == 18)
            {
                VoyantAlt.ForeColor = System.Drawing.Color.Gray;
                //Console.WriteLine("Touche ALT relâchée.");
            }
            //If shift key was pressed, it's not a number.
            //if ((e.KeyData & Keys.ControlKey) == Keys.ControlKey)
            if ((e.KeyValue) == 17)
            {
                VoyantControl.ForeColor = System.Drawing.Color.Gray;
                //Console.WriteLine("Touche CONTROL relâchée.");
            }
            if (e.KeyData == Keys.Menu)
            {
                //YourCode
                //VoyantAlt.ForeColor = System.Drawing.Color.Gray;
                //Console.WriteLine("Touche ALT relâchée.");
                e.Handled = true;
            }

            //Console.WriteLine("Touche {0} relâchée.", e.KeyData);
            //Console.WriteLine("<---");
            tbReponse.Text = "";
        }
    
        private Keys raccourciContient(string key)
        {
            Keys retour = 0;

            #region Touches de contrôle
            // ------------------------------------
            // Touches de contrôle
            // ------------------------------------
            if (key.Contains("ctrl+") == true)
            {
                retour = retour | Keys.Control;
            }
            if (key.Contains("alt+") == true)
            {
                retour = retour | Keys.Alt;
            }
            if (key.Contains("shift+") == true)
            {
                retour = retour | Keys.Shift;
            }
            #endregion
            
            #region Touches spéciales
            // ----------------------------------------
            // Touches spéciales (enter, suppr, etc...)
            // ----------------------------------------
            if (key.Contains("+escape") == true)
            {
                retour = retour | Keys.Escape;
            }
            if (key.Contains("+enter") == true)
            {
                retour = retour | Keys.Enter;
            }
            if (key.Contains("+tab") == true)
            {
                retour = retour | Keys.Tab;
            }
            if (key.Contains("+space") == true)
            {
                retour = retour | Keys.Space;
            }
            if (key.Contains("+backspace") == true)
            {
                retour = retour | Keys.Back;
            }
            if (key.Contains("+capslock") == true)
            {
                retour = retour | Keys.CapsLock;
            }
            if (key.Contains("+ins") == true)
            {
                retour = retour | Keys.Insert;
            }
            if (key.Contains("+delete") == true)
            {
                retour = retour | Keys.Delete;
            }
            if (key.Contains("+home") == true)
            {
                retour = retour | Keys.Home;
            }
            if (key.Contains("+end") == true)
            {
                retour = retour | Keys.End;
            }
            if (key.Contains("+pageup") == true)
            {
                retour = retour | Keys.PageUp;
            }
            if (key.Contains("+pagedown") == true)
            {
                retour = retour | Keys.PageDown;
            }
            if (key.Contains("+prtscrn") == true)
            {
                retour = retour | Keys.PrintScreen;
            }
            if (key.Contains("+scroll") == true)
            {
                retour = retour | Keys.Scroll;
            }
            if (key.Contains("+attn") == true)
            {
                retour = retour | Keys.Attn;
            }
            if (key.Contains("+*") == true)
            {
                retour = retour | Keys.Multiply;
            }
            #endregion

            #region Touches de direction
            // ----------------------------------------
            // Touches de direction
            // ----------------------------------------
            if (key.Contains("+down") == true)
            {
                retour = retour | Keys.Down;
            }
            if (key.Contains("+up") == true)
            {
                retour = retour | Keys.Up;
            }
            if (key.Contains("+right") == true)
            {
                retour = retour | Keys.Right;
            }
            if (key.Contains("+left") == true)
            {
                retour = retour | Keys.Left;
            }
            #endregion

            #region Touches de fonctions
            // ------------------------------------
            // Touches de fonctions (F1, F2, etc ...)
            // ------------------------------------
            if (key.Contains("+F1") == true)
            {
                retour = retour | Keys.F1;
            }
            if (key.Contains("+F2") == true)
            {
                retour = retour | Keys.F2;
            }
            if (key.Contains("+F3") == true)
            {
                retour = retour | Keys.F3;
            }
            if (key.Contains("+F4") == true)
            {
                retour = retour | Keys.F4;
            }
            if (key.Contains("+F5") == true)
            {
                retour = retour | Keys.F5;
            }
            if (key.Contains("+F6") == true)
            {
                retour = retour | Keys.F6;
            }
            if (key.Contains("+F7") == true)
            {
                retour = retour | Keys.F7;
            }
            if (key.Contains("+F8") == true)
            {
                retour = retour | Keys.F8;
            }
            if (key.Contains("+F9") == true)
            {
                retour = retour | Keys.F9;
            }
            if (key.Contains("+F10") == true)
            {
                retour = retour | Keys.F10;
            }
            if (key.Contains("+F11") == true)
            {
                retour = retour | Keys.F11;
            }
            if (key.Contains("+F12") == true)
            {
                retour = retour | Keys.F12;
            }
            if (key.Contains("+F13") == true)
            {
                retour = retour | Keys.F13;
            }
            if (key.Contains("+F14") == true)
            {
                retour = retour | Keys.F14;
            }
            if (key.Contains("+F15") == true)
            {
                retour = retour | Keys.F15;
            }
            if (key.Contains("+F16") == true)
            {
                retour = retour | Keys.F16;
            }
            if (key.Contains("+F17") == true)
            {
                retour = retour | Keys.F17;
            }
            if (key.Contains("+F18") == true)
            {
                retour = retour | Keys.F18;
            }
            if (key.Contains("+F19") == true)
            {
                retour = retour | Keys.F19;
            }
            if (key.Contains("+F20") == true)
            {
                retour = retour | Keys.F20;
            }
            if (key.Contains("+F21") == true)
            {
                retour = retour | Keys.F21;
            }
            if (key.Contains("+F22") == true)
            {
                retour = retour | Keys.F22;
            }
            if (key.Contains("+F23") == true)
            {
                retour = retour | Keys.F23;
            }
            if (key.Contains("+F24") == true)
            {
                retour = retour | Keys.F24;
            }
            #endregion

            #region Chiffres
            // ------------------------------------
            // Chiffres
            // ------------------------------------
            if (key.Contains("+0") == true)
            {
                retour = retour | Keys.D0;
            }
            if (key.Contains("+1") == true)
            {
                retour = retour | Keys.D1;
            }
            if (key.Contains("+2") == true)
            {
                retour = retour | Keys.D2;
            }
            if (key.Contains("+3") == true)
            {
                retour = retour | Keys.D3;
            }
            if (key.Contains("+4") == true)
            {
                retour = retour | Keys.D4;
            }
            if (key.Contains("+5") == true)
            {
                retour = retour | Keys.D5;
            }
            if (key.Contains("+6") == true)
            {
                retour = retour | Keys.D6;
            }
            if (key.Contains("+7") == true)
            {
                retour = retour | Keys.D7;
            }
            if (key.Contains("+8") == true)
            {
                retour = retour | Keys.D8;
            }
            if (key.Contains("+9") == true)
            {
                retour = retour | Keys.D9;
            }


            // ------------------------------------
            // Chiffres du pavé numérique
            // ------------------------------------
            if (key.Contains("+0") == true)
            {
                retour = retour | Keys.NumPad0;
            }
            if (key.Contains("+1") == true)
            {
                retour = retour | Keys.NumPad1;
            }
            if (key.Contains("+2") == true)
            {
                retour = retour | Keys.NumPad2;
            }
            if (key.Contains("+3") == true)
            {
                retour = retour | Keys.NumPad3;
            }
            if (key.Contains("+4") == true)
            {
                retour = retour | Keys.NumPad4;
            }
            if (key.Contains("+5") == true)
            {
                retour = retour | Keys.NumPad5;
            }
            if (key.Contains("+6") == true)
            {
                retour = retour | Keys.NumPad6;
            }
            if (key.Contains("+7") == true)
            {
                retour = retour | Keys.NumPad7;
            }
            if (key.Contains("+8") == true)
            {
                retour = retour | Keys.NumPad8;
            }
            if (key.Contains("+9") == true)
            {
                retour = retour | Keys.NumPad9;
            }
            #endregion
            
            #region Lettres de l'alphabet
            // ------------------------------------
            // Lettres de l'alphabet
            // ------------------------------------
            if (key.Contains("+A") == true)
            {
                retour = retour | Keys.A;
            }
            if (key.Contains("+B") == true)
            {
                retour = retour | Keys.B;
            }
            if (key.Contains("+C") == true)
            {
                retour = retour | Keys.C;
            }
            if (key.Contains("+D") == true)
            {
                retour = retour | Keys.D;
            }
            if (key.Contains("+E") == true)
            {
                retour = retour | Keys.E;
            }
            if (key.Contains("+F") == true)
            {
                retour = retour | Keys.F;
            }
            if (key.Contains("+G") == true)
            {
                retour = retour | Keys.G;
            }
            if (key.Contains("+H") == true)
            {
                retour = retour | Keys.H;
            }
            if (key.Contains("+I") == true)
            {
                retour = retour | Keys.I;

            }
            if (key.Contains("+J") == true)
            {
                retour = retour | Keys.J;

            }
            if (key.Contains("+K") == true)
            {
                retour = retour | Keys.K;

            }
            if (key.Contains("+L") == true)
            {
                retour = retour | Keys.L;

            }
            if (key.Contains("+M") == true)
            {
                retour = retour | Keys.M;

            }
            if (key.Contains("+N") == true)
            {
                retour = retour | Keys.N;

            }
            if (key.Contains("+O") == true)
            {
                retour = retour | Keys.O;

            }
            if (key.Contains("+P") == true)
            {
                retour = retour | Keys.P;

            }
            if (key.Contains("+Q") == true)
            {
                retour = retour | Keys.Q;

            }
            if (key.Contains("+R") == true)
            {
                retour = retour | Keys.R;

            }
            if (key.Contains("+S") == true)
            {
                retour = retour | Keys.S;

            }
            if (key.Contains("+T") == true)
            {
                retour = retour | Keys.T;

            }
            if (key.Contains("+U") == true)
            {
                retour = retour | Keys.U;

            }
            if (key.Contains("+V") == true)
            {
                retour = retour | Keys.V;

            }
            if (key.Contains("+W") == true)
            {
                retour = retour | Keys.W;

            }
            if (key.Contains("+X") == true)
            {
                retour = retour | Keys.X;

            }
            if (key.Contains("+Y") == true)
            {
                retour = retour | Keys.Y;

            }
            if (key.Contains("+Z") == true)
            {
                retour = retour | Keys.Z;

            }
            #endregion


            //else
            //{
            //    Console.WriteLine("Warning, le raccourci ne contient ni ctrl, ni alt, ni shift !");
            //}
            return retour;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (btnDemarrerArreter.Text == "Démarrer")
            {
                btnDemarrerArreter.Text = "Arrêter";
                reponseTimer.Start();
            }
            else
            {
                btnDemarrerArreter.Text = "Démarrer";
                reponseTimer.Stop();
            }
            
            
        }

        private string format_Input(string userInput)
        {
            string sthing = "";
            string control = "ctrl";
            string alt = "alt";
            string maj = "shift";
            string retour = "";

            if (userInput.Contains(", Control"))
            {
                retour = retour + control;
            }
            if (userInput.Contains(", Alt"))
            {
                if (retour == "")
                {
                    retour = retour + alt;
                }
                else
                {
                    retour = retour + "+" + alt;
                }
            }
            if (userInput.Contains(", Shift"))
            {
                if (retour == "")
                {
                    retour = retour + maj;
                }
                else
                {
                    retour = retour + "+" + maj;
                }
            }

            sthing = userInput.Substring(0, 1);
            retour = retour + "+" + sthing;

            return retour;
        }


    
    }
}
