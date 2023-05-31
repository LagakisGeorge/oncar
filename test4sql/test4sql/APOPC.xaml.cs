using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Net.Mobile.Forms;
using System.Threading;
using Mono.Data.Sqlite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Plugin.Toast;
using SharpCifs.Smb;
using System.Data.SqlClient;
using System.Data;

namespace test4sql
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class techn1 : ContentPage
    {
        public SqlConnection con;
        public IList<Monkey> Monkeys { get; private set; }
        public string f_cid="" ;
        public techn1()
        {
            InitializeComponent();
            Monkeys = new List<Monkey>();
        }


        async void WriteFile(object sender, EventArgs e)
        {

                //  -----------------SQLSERVER  1.SYNDESH   ---------------------------------------
                if (Globals.cSQLSERVER.Length<2)
            {
                await DisplayAlert("ΔΕΝ ΔΗΛΩΘΗΚΕ Ο SERVER", "ΠΑΤΕ ΠΑΡΑΜΕΤΡΟΙ", "OK");
                return;
            }
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            con = new SqlConnection(constring);
            try
            {
                con.Open();
                // ***************  demo πως τρεχω εντολη στον sqlserver ********************************
               // SqlCommand cmd = new SqlCommand("insert into PALETES(PALET) values (1)");
               // cmd.Connection = con;
               // cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }




            var action = await DisplayAlert("Θα διαγραφουν τα τιμολόγια του κινητού", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {

            }
            else
            {
                return;
            }


            MainPage.ExecuteSqlite("delete from EID;");
            MainPage.ExecuteSqlite("delete from TIM;");
            MainPage.ExecuteSqlite("delete from EGGTIM;");

            String SYNT = "";
            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                SYNT= " TOP 20 ";



            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select "+SYNT+ " ID,isnull(KOD,'') AS MKOD,ISNULL(ONO,'') AS MONO,ISNULL(ERG,'') AS MERG,ISNULL(LTI,0) AS MLTI,ISNULL(MON,'TEM') AS MMON,ISNULL(FPA,1) AS MFPA,ISNULL(XTI,0) AS MXTI  FROM EID ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
               // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    String mF = dt.Rows[k]["MONO"].ToString();
                    mF = mF.Replace("'", "`");

                    String MMON = dt.Rows[k]["MMON"].ToString();
                    MMON = MMON.Replace("'", "`");

                    string MFPA = dt.Rows[k]["MFPA"].ToString();
                    MFPA = MFPA.Replace(",", ".");


                    // MyList.Add(mF);
                    string mTYP = dt.Rows[k]["MLTI"].ToString();
                    mTYP = mTYP.Replace(",", ".");




                    string mXTI = dt.Rows[k]["MXTI"].ToString();
                    mXTI = mXTI.Replace(",", ".");



                    /* Monkeys.Add(new Monkey
                    {
                        Name = mF,
                        Location = dt.Rows[k]["MKOD"].ToString(),
                        ImageUrl = mTYP, // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                        idPEL = dt.Rows[k]["ID"].ToString()
                    });
                    */
                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                    //string mONO = dt.Rows[k]["MONO"].ToString();
                    string mERG = dt.Rows[k]["MERG"].ToString();
                    mERG = mERG.Replace("'", "`");

                    mTYP = mTYP.Replace(",", ".");
                    int n2 = MainPage.ExecuteSqlite("insert into EID (XTI,XONDR,KOD,ONO,BARCODE,MON,FPA) VALUES ("+mXTI+"," + mTYP + ",'" + mKOD + "','" + mF + "','" + mERG + "','"+MMON+"',"+MFPA+");");
                } // FOR

                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;
               // await DisplayAlert("EIΔΗ", " Eιδη:" + dt.Rows.Count, "OK");
                EID.Text = " ΕΙΔΗ:" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }


            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                 await DisplayAlert("EIΔΗ ΟΚ", "", "OK");

            MainPage.ExecuteSqlite("delete from PEL;");
            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select "+SYNT+" LEFT(ISNULL(DOY,''),20) AS MDOY,ID,isnull(KOD,'') AS MKOD,LEFT(ISNULL(EPO,''),25) AS MEPO,LEFT(ISNULL(EPA,''),20) AS MEPA," +
                    "ISNULL(DIE,'') AS MDIE,ISNULL(TYP,0) AS MYPOL ,ISNULL(DOY,'') AS MDOY,ISNULL(AFM,'') AS MAFM,ISNULL(POL,'') AS MPOL,ISNULL(PEK,0) AS MPEK,ISNULL(NUM1,0) AS MNUM1 ,ISNULL(CH3,'ΕΔΡΑ ΠΕΛΑΤΗ') AS PARAD ,ISNULL(AYP,0) AS AYP  " +
                    "FROM PEL WHERE EIDOS='e' " , con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                //List<string> MyList = new List<string>();
                int k = 0;

                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    // String mF = dt.Rows[k]["MEPO"].ToString();
                    // mF = mF.Replace("'", "`");
                    // MyList.Add(mF);
                    string mTYP2;
                    mTYP2 = dt.Rows[k]["MYPOL"].ToString();

                    if (mTYP2 == null)
                    {
                        mTYP2 = "0";
                    }
                    //else
                    mTYP2 = mTYP2.Replace(",", ".");

                    string mAYP2;
                    mAYP2 = dt.Rows[k]["AYP"].ToString();

                    if (mAYP2 == null)
                    {
                        mAYP2 = "0";
                    }
                    //else
                    mAYP2 = mAYP2.Replace(",", ".");






                    /* Monkeys.Add(new Monkey
                    {
                       Name = mF,
                       Location = dt.Rows[k]["MKOD"].ToString(),
                       ImageUrl = mTYP, // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                       idPEL = dt.Rows[k]["ID"].ToString()
                     });
                      */
                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                    //string mONO = dt.Rows[k]["MONO"].ToString();
                    string mDIE = dt.Rows[k]["MDIE"].ToString();
                    mDIE = mDIE.Replace("'", "`");

                    string mEPO = dt.Rows[k]["MEPO"].ToString();
                    mEPO = mEPO.Replace("'", "`");

                    string mPOL = dt.Rows[k]["MPOL"].ToString();
                    mPOL = mPOL.Replace("'", "`");

                    string mAFM = dt.Rows[k]["MAFM"].ToString();
                    mAFM = mAFM.Replace("'", "`");

                    string mDOY = dt.Rows[k]["MDOY"].ToString();
                    mDOY = mDOY.Replace("'", "`");

                    string mPEK = dt.Rows[k]["MPEK"].ToString();

                    string mEPA = dt.Rows[k]["MEPA"].ToString();
                    mEPA = mEPA.Replace("'", "`");

                    string mPARAD = dt.Rows[k]["PARAD"].ToString();
                    mPARAD = mPARAD.Replace("'", "`");


                    string mNUM1 = dt.Rows[k]["MNUM1"].ToString();
                    if (mNUM1.All(char.IsNumber) ==false) { mNUM1 = "0"; }
                        mNUM1 = mNUM1.Replace(",", ".");


                    int n2 = MainPage.ExecuteSqlite("insert into PEL (R1,KOD,EPA,AFM,DOY,EPO,DIE,TYP,PEK,NUM1,CH3) VALUES ("
                     + mAYP2 + ",'" + mKOD + "','" + mEPA + "','" + mAFM + "','" + mDOY + "','" + mEPO + "','" + mDIE + "'," + mTYP2 + "," + mPEK + "," + mNUM1 + ",'" + mPARAD + "' );");
                    // FOR
                }
                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;
                //  await DisplayAlert("ΠΕΛΑΤΕΣ", " Πελάτες:" + dt.Rows.Count, "OK");
                PEL.Text = " ΠΕΛΑΤΕΣ:" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }


            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                await DisplayAlert("ΠΕΛΑΤΕΣ ΟΚ", "", "OK");


            //   TIMOKAT( [KOD][nvarchar](14) NOT NULL,"+
            //       "[EKPT] [decimal](5, 2) NOT NULL, [TIMOK]


            MainPage.ExecuteSqlite("delete from TIMOKAT;");
            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select  isnull(KOD,'') AS MKOD,ISNULL(EKPT,0) AS MEKPT,ISNULL(TIMOK,0) AS MTIMOK  FROM TIMOKAT ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {

                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                    //string mONO = dt.Rows[k]["MONO"].ToString();

                    // MyList.Add(mF);
                    string mTYP = dt.Rows[k]["MEKPT"].ToString();

                    if (mTYP == null)
                    {
                        mTYP = "0";
                    }



                    mTYP = mTYP.Replace(",", ".");

                    string mTIMOK = dt.Rows[k]["MTIMOK"].ToString();
                    mTIMOK = mTIMOK.Replace(",", ".");


                    /* Monkeys.Add(new Monkey
                    {
                        Name = mF,
                        Location = dt.Rows[k]["MKOD"].ToString(),
                        ImageUrl = mTYP, // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                        idPEL = dt.Rows[k]["ID"].ToString()
                    });
                    */

                    int n2 = MainPage.ExecuteSqlite("insert into TIMOKAT(TIMOKID,EKPT,KOD,TIMOK) VALUES (0," + mTYP + ",'" + mKOD + "'," + mTIMOK + ");");
                } // FOR

                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;
               // await DisplayAlert("TIMOK", " TIMOK:" + dt.Rows.Count, "OK");
                TIMOKAT.Text  = " TIMOK:" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }




            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                await DisplayAlert("ΤΙΜΟΚΑΤΑΛΟΓΟΙ ΟΚ", "", "OK");














        }


        async void Write2File(object sender, EventArgs e)
        {











            //  -----------------SQLSERVER  1.SYNDESH   ---------------------------------------
            if (Globals.cSQLSERVER.Length < 2)
            {
                await DisplayAlert("ΔΕΝ ΔΗΛΩΘΗΚΕ Ο SERVER", "ΠΑΤΕ ΠΑΡΑΜΕΤΡΟΙ", "OK");
                return;
            }
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            con = new SqlConnection(constring);
            try
            {
                con.Open();
                // ***************  demo πως τρεχω εντολη στον sqlserver ********************************
                // SqlCommand cmd = new SqlCommand("insert into PALETES(PALET) values (1)");
                // cmd.Connection = con;
                // cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }




            var action = await DisplayAlert("Θα διαγραφουν τα τιμολόγια του κινητού", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {

            }
            else
            {
                return;
            }


            MainPage.ExecuteSqlite("delete from EID;");
            MainPage.ExecuteSqlite("delete from TIM;");
            MainPage.ExecuteSqlite("delete from EGGTIM;");

            String SYNT = "";
            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                SYNT = " TOP 20 ";



            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select " + SYNT + " EID.ID,isnull(EID.KOD,'') AS MKOD,ISNULL(ONO,'') AS MONO,ISNULL(BARCODES.ERG,'') AS MERG,ISNULL(LTI,0) AS MLTI,ISNULL(MON,'TEM') AS MMON,ISNULL(FPA,1) AS MFPA,ISNULL(XTI,0) AS MXTI  FROM EID LEFT JOIN BARCODES ON EID.KOD=BARCODES.KOD WHERE ENERGO=1 ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    String mF = dt.Rows[k]["MONO"].ToString();
                    mF = mF.Replace("'", "`");

                    String MMON = dt.Rows[k]["MMON"].ToString();
                    MMON = MMON.Replace("'", "`");

                    string MFPA = dt.Rows[k]["MFPA"].ToString();
                    MFPA = MFPA.Replace(",", ".");


                    // MyList.Add(mF);
                    string mTYP = dt.Rows[k]["MLTI"].ToString();
                    mTYP = mTYP.Replace(",", ".");




                    string mXTI = dt.Rows[k]["MXTI"].ToString();
                    mXTI = mXTI.Replace(",", ".");



                    /* Monkeys.Add(new Monkey
                    {
                        Name = mF,
                        Location = dt.Rows[k]["MKOD"].ToString(),
                        ImageUrl = mTYP, // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                        idPEL = dt.Rows[k]["ID"].ToString()
                    });
                    */
                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                    //string mONO = dt.Rows[k]["MONO"].ToString();
                    string mERG = dt.Rows[k]["MERG"].ToString();
                    mERG = mERG.Replace("'", "`");

                    mTYP = mTYP.Replace(",", ".");
                    int n2 = MainPage.ExecuteSqlite("insert into EID (XTI,XONDR,KOD,ONO,BARCODE,MON,FPA) VALUES (" + mXTI + "," + mTYP + ",'" + mKOD + "','" + mF + "','" + mERG + "','" + MMON + "'," + MFPA + ");");
                } // FOR

                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;
                // await DisplayAlert("EIΔΗ", " Eιδη:" + dt.Rows.Count, "OK");
                EID.Text = " ΕΙΔΗ:" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }


            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                await DisplayAlert("EIΔΗ ΟΚ", "", "OK");

            MainPage.ExecuteSqlite("delete from PEL;");
            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select " + SYNT + " LEFT(ISNULL(DOY,''),20) AS MDOY,ID,isnull(KOD,'') AS MKOD,LEFT(ISNULL(EPO,''),25) AS MEPO,LEFT(ISNULL(EPA,''),20) AS MEPA," +
                    "ISNULL(DIE,'') AS MDIE,ISNULL(TYP,0) AS MYPOL ,ISNULL(DOY,'') AS MDOY,ISNULL(AFM,'') AS MAFM,ISNULL(POL,'') AS MPOL,ISNULL(PEK,0) AS MPEK,ISNULL(NUM1,0) AS MNUM1 ,ISNULL(CH3,'ΕΔΡΑ ΠΕΛΑΤΗ') AS PARAD ,ISNULL(AYP,0) AS AYP  " +
                    "FROM PEL WHERE EIDOS='e' ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                //List<string> MyList = new List<string>();
                int k = 0;

                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    // String mF = dt.Rows[k]["MEPO"].ToString();
                    // mF = mF.Replace("'", "`");
                    // MyList.Add(mF);
                    string mTYP2;
                    mTYP2 = dt.Rows[k]["MYPOL"].ToString();

                    if (mTYP2 == null)
                    {
                        mTYP2 = "0";
                    }
                    //else
                    mTYP2 = mTYP2.Replace(",", ".");

                    string mAYP2;
                    mAYP2 = dt.Rows[k]["AYP"].ToString();

                    if (mAYP2 == null)
                    {
                        mAYP2 = "0";
                    }
                    //else
                    mAYP2 = mAYP2.Replace(",", ".");






                    /* Monkeys.Add(new Monkey
                    {
                       Name = mF,
                       Location = dt.Rows[k]["MKOD"].ToString(),
                       ImageUrl = mTYP, // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                       idPEL = dt.Rows[k]["ID"].ToString()
                     });
                      */
                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                    //string mONO = dt.Rows[k]["MONO"].ToString();
                    string mDIE = dt.Rows[k]["MDIE"].ToString();
                    mDIE = mDIE.Replace("'", "`");

                    string mEPO = dt.Rows[k]["MEPO"].ToString();
                    mEPO = mEPO.Replace("'", "`");

                    string mPOL = dt.Rows[k]["MPOL"].ToString();
                    mPOL = mPOL.Replace("'", "`");

                    string mAFM = dt.Rows[k]["MAFM"].ToString();
                    mAFM = mAFM.Replace("'", "`");

                    string mDOY = dt.Rows[k]["MDOY"].ToString();
                    mDOY = mDOY.Replace("'", "`");

                    string mPEK = dt.Rows[k]["MPEK"].ToString();

                    string mEPA = dt.Rows[k]["MEPA"].ToString();
                    mEPA = mEPA.Replace("'", "`");

                    string mPARAD = dt.Rows[k]["PARAD"].ToString();
                    mPARAD = mPARAD.Replace("'", "`");


                    string mNUM1 = dt.Rows[k]["MNUM1"].ToString();
                    if (mNUM1.All(char.IsNumber) == false) { mNUM1 = "0"; }
                    mNUM1 = mNUM1.Replace(",", ".");


                    int n2 = MainPage.ExecuteSqlite("insert into PEL (R1,KOD,EPA,AFM,DOY,EPO,DIE,TYP,PEK,NUM1,CH3) VALUES ("
                     + mAYP2 + ",'" + mKOD + "','" + mEPA + "','" + mAFM + "','" + mDOY + "','" + mEPO + "','" + mDIE + "'," + mTYP2 + "," + mPEK + "," + mNUM1 + ",'" + mPARAD + "' );");
                    // FOR
                }
                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;
                //  await DisplayAlert("ΠΕΛΑΤΕΣ", " Πελάτες:" + dt.Rows.Count, "OK");
                PEL.Text = " ΠΕΛΑΤΕΣ:" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }


            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                await DisplayAlert("ΠΕΛΑΤΕΣ ΟΚ", "", "OK");


            //   TIMOKAT( [KOD][nvarchar](14) NOT NULL,"+
            //       "[EKPT] [decimal](5, 2) NOT NULL, [TIMOK]


            MainPage.ExecuteSqlite("delete from TIMOKAT;");
            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select  isnull(KOD,'') AS MKOD,ISNULL(EKPT,0) AS MEKPT,ISNULL(TIMOK,0) AS MTIMOK  FROM TIMOKAT ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {

                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                    //string mONO = dt.Rows[k]["MONO"].ToString();

                    // MyList.Add(mF);
                    string mTYP = dt.Rows[k]["MEKPT"].ToString();

                    if (mTYP == null)
                    {
                        mTYP = "0";
                    }



                    mTYP = mTYP.Replace(",", ".");

                    string mTIMOK = dt.Rows[k]["MTIMOK"].ToString();
                    mTIMOK = mTIMOK.Replace(",", ".");


                    /* Monkeys.Add(new Monkey
                    {
                        Name = mF,
                        Location = dt.Rows[k]["MKOD"].ToString(),
                        ImageUrl = mTYP, // "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                        idPEL = dt.Rows[k]["ID"].ToString()
                    });
                    */

                    int n2 = MainPage.ExecuteSqlite("insert into TIMOKAT(TIMOKID,EKPT,KOD,TIMOK) VALUES (0," + mTYP + ",'" + mKOD + "'," + mTIMOK + ");");
                } // FOR

                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;
                // await DisplayAlert("TIMOK", " TIMOK:" + dt.Rows.Count, "OK");
                TIMOKAT.Text = " TIMOK:" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }




            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                await DisplayAlert("ΤΙΜΟΚΑΤΑΛΟΓΟΙ ΟΚ", "", "OK");














        }

        private void dokimi(object sender, EventArgs e)
        {

            TEST2.BackgroundColor = Xamarin.Forms.Color.Green;
        }

        async void CKARTELA32(object sender, EventArgs e) 
        { 

         

            //  -----------------SQLSERVER  1.SYNDESH   ---------------------------------------
            if (Globals.cSQLSERVER.Length < 2)
            {
                await DisplayAlert("ΔΕΝ ΔΗΛΩΘΗΚΕ Ο SERVER", "ΠΑΤΕ ΠΑΡΑΜΕΤΡΟΙ", "OK");
                return;
            }
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            con = new SqlConnection(constring);
            try
            {
                con.Open();
                // ***************  demo πως τρεχω εντολη στον sqlserver ********************************
                // SqlCommand cmd = new SqlCommand("insert into PALETES(PALET) values (1)");
                // cmd.Connection = con;
                // cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }




            var action = await DisplayAlert("Θα διαγραφουν οι κινησεις καρτελας του κινητού", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {

            }
            else
            {
                return;
            }


            MainPage.ExecuteSqlite("delete from EGG;");
  
            String SYNT = "";
            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                SYNT = " TOP 20 ";



            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select " + SYNT + " ID,isnull(KOD,'') AS MKOD,ISNULL(ATIM,'') AS MATIM,ISNULL(XREOSI,0) AS MXRE,ISNULL(PISTOSI,0) AS MPIS,CONVERT(CHAR(10),HME,103) AS MHME  FROM EGG  WHERE EIDOS='e'  ORDER BY KOD,HME ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    String mATIM= dt.Rows[k]["MATIM"].ToString();
                    mATIM = mATIM.Replace("'", "`");


                    String mKOD = dt.Rows[k]["MKOD"].ToString();
                    mKOD = mKOD.Replace("'", "`");



                    String mHME = dt.Rows[k]["MHME"].ToString();
                    mHME = mHME.Replace("'", "`");

                    string mXRE = dt.Rows[k]["MXRE"].ToString();
                    mXRE = mXRE.Replace(",", ".");


                    // MyList.Add(mF);
                    string mPIS = dt.Rows[k]["MPIS"].ToString();
                    mPIS = mPIS.Replace(",", ".");


                    int n2 = MainPage.ExecuteSqlite("insert into EGG (KOD,HME,AIT,XRE,PIS) VALUES ('" + mKOD + "','" + mHME + "','" + mATIM + "'," + mXRE + "," + mPIS  + ");");
                } // FOR



                MainPage.ExecuteSqlite("UPDATE EGG SET IDPEL=(SELECT ID FROM PEL WHERE KOD=EGG.KOD) ;");

                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;
                // await DisplayAlert("EIΔΗ", " Eιδη:" + dt.Rows.Count, "OK");
                EID.Text = " KINHΣEIΣ:" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }


            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                await DisplayAlert("KINHΣΕΙΣ ΟΚ", "", "OK");







        }

        private async void BarWriteFile(object sender, EventArgs e)
        {


            //  -----------------SQLSERVER  1.SYNDESH   ---------------------------------------
            if (Globals.cSQLSERVER.Length < 2)
            {
                await DisplayAlert("ΔΕΝ ΔΗΛΩΘΗΚΕ Ο SERVER", "ΠΑΤΕ ΠΑΡΑΜΕΤΡΟΙ", "OK");
                return;
            }
            string[] lines = Globals.cSQLSERVER.Split(';');
            string constring = @"Data Source=" + lines[0] + ";Initial Catalog=" + lines[1] + ";Uid=sa;Pwd=" + lines[2]; // ";Initial Catalog=MERCURY;Uid=sa;Pwd=12345678";

            // string constring = @"Data Source=" + Globals.cSQLSERVER + ";Initial Catalog=TECHNOPLASTIKI;Uid=sa;Pwd=12345678";
            con = new SqlConnection(constring);
            try
            {
                con.Open();
                // ***************  demo πως τρεχω εντολη στον sqlserver ********************************
                // SqlCommand cmd = new SqlCommand("insert into PALETES(PALET) values (1)");
                // cmd.Connection = con;
                // cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                await DisplayAlert("ΑΔΥΝΑΜΙΑ ΣΥΝΔΕΣΗΣ", ex.ToString(), "OK");
            }




            var action = await DisplayAlert("Θα διαγραφουν τα ειδη του κινητού", "Εισαι σίγουρος?", "Ναι", "Οχι");
            if (action)
            {

            }
            else
            {
                return;
            }


            MainPage.ExecuteSqlite("delete from EIDH;");
            MainPage.ExecuteSqlite("delete from KATHG;");
            MainPage.ExecuteSqlite("delete from TABLES;");

            String SYNT = "";
            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                SYNT = " TOP 20 ";

      //  CH2    ,[NUM1]
      //,[NUM2]
      //,[TIMH]
      //,[KATHG]

            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select " + SYNT + " ID,isnull(KOD,'') AS MKOD,ISNULL(ONO,'') AS MONO,ISNULL(CH2,'') AS MCH2,ISNULL(TIMH,0) AS MTIMH,ISNULL(NUM1,0) AS MNUM1,ISNULL(NUM2,0) AS MNUM2,ISNULL(KATHG,0) AS MKATHG  FROM EIDH ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    String mF = dt.Rows[k]["MONO"].ToString();
                    mF = mF.Replace("'", "`");

                    String MCH2 = dt.Rows[k]["MCH2"].ToString();
                    MCH2 = MCH2.Replace("'", "`");

                    string MTIMH = dt.Rows[k]["MTIMH"].ToString();
                    MTIMH = MTIMH.Replace(",", ".");

                    string MKATHG = dt.Rows[k]["MKATHG"].ToString();
                    MKATHG = MKATHG.Replace(",", ".");

                    // MyList.Add(mF);
                    string mNUM1 = dt.Rows[k]["MNUM1"].ToString();
                    mNUM1 = mNUM1.Replace(",", ".");

                    string mNUM2 = dt.Rows[k]["MNUM2"].ToString();
                    mNUM2 = mNUM2.Replace(",", ".");


                    string mKOD = dt.Rows[k]["MKOD"].ToString();
                   int n2 = MainPage.ExecuteSqlite("insert into EIDH (NUM2,TIMH,NUM1,ONO,KATHG) VALUES ("+mNUM2+"," + MTIMH + "," + mNUM1 + ",'" + mF + "'," + MKATHG + ");");
                } // FOR

                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;
                // await DisplayAlert("EIΔΗ", " Eιδη:" + dt.Rows.Count, "OK");
                EID.Text = " ΕΙΔΗ:" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }


            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                await DisplayAlert("EIΔΗ ΟΚ", "", "OK");


            //-------------------------------------------------------------------------------

            MainPage.ExecuteSqlite("delete from KATHG;");
            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select " + SYNT + " LEFT(ISNULL(ONO,''),20) AS MONO,ID as KOD FROM KATHG", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                //List<string> MyList = new List<string>();
                int k = 0;

                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {
                    // String mF = dt.Rows[k]["MEPO"].ToString();
                    // mF = mF.Replace("'", "`");
                    // MyList.Add(mF);
                    string MKOD;
                    MKOD = dt.Rows[k]["KOD"].ToString();
                    MKOD = MKOD.Replace(",", ".");


                    string MONO = dt.Rows[k]["MONO"].ToString();
                    MONO = MONO.Replace("'", "`");

                  
                    int n2 = MainPage.ExecuteSqlite("insert into KATHG (KOD,ONO) VALUES (" +MKOD+",'"+MONO+ "' );");
                    
                }

                PEL.Text = " KATHGORIES:" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }


            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                await DisplayAlert("ΠΕΛΑΤΕΣ ΟΚ", "", "OK");




            //------------------------------------------------------------------------------------
            // [ONO],[KATEILHMENO],[NUM1],[NUM2],[CH1],[IDPARAGG]
            MainPage.ExecuteSqlite("delete from TABLES;");
            try
            {
                Monkeys = new List<Monkey>();
                DataTable dt = new DataTable();
                SqlCommand cmd3 = new SqlCommand("select  isnull(ONO,'') AS MONO,ISNULL(KATEILHMENO,0) AS MKAT,ISNULL(NUM1,0) AS MNUM1,ISNULL(NUM2,0) AS MNUM2,ISNULL(CH1,'') AS MCH1,ISNULL(IDPARAGG,0) AS MIDPARAGG  FROM TABLES ", con);
                var adapter2 = new SqlDataAdapter(cmd3);
                adapter2.Fill(dt);
                // List<string> MyList = new List<string>();
                int k = 0;
                for (k = 0; k <= dt.Rows.Count - 1; k++)
                {

                    string MONO = dt.Rows[k]["MONO"].ToString();
                    MONO = MONO.Replace("'", "`");

                    string MKAT = dt.Rows[k]["MKAT"].ToString();
                    MKAT = MKAT.Replace(",", ".");

                    string MNUM1 = dt.Rows[k]["MNUM1"].ToString();
                    MNUM1 = MNUM1.Replace(",", ".");

                    string MNUM2 = dt.Rows[k]["MNUM2"].ToString();
                    MNUM2 = MNUM2.Replace(",", ".");

                    string MIDPARAGG = dt.Rows[k]["MIDPARAGG"].ToString();
                    MIDPARAGG = MIDPARAGG.Replace(",", ".");

                    int n2 = MainPage.ExecuteSqlite("insert into TABLES(ONO,KATEILHMENO,NUM1,NUM2,IDPARAGG) VALUES ('"+MONO+"',"+MKAT+"," +MNUM1 + "," + MNUM2 + "," + MIDPARAGG  + ");");
                } // FOR

                // watch.Stop();
                //  var elapsedMs = watch.ElapsedMilliseconds;
                // await DisplayAlert("TIMOK", " TIMOK:" + dt.Rows.Count, "OK");
                TIMOKAT.Text = " ΤΡΑΠΕΖΙΑ :" + dt.Rows.Count;

                BindingContext = this;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }




            if (TEST2.BackgroundColor == Xamarin.Forms.Color.Green)
                await DisplayAlert("ΤΡΑΠΕΖΙΑ ΟΚ", "", "OK");




        }















    }
}