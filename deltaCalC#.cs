using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deltaKinematics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class calibrate
        {
            //RETRIEVE DATA
            double A = double.TryParse(document.getElementById('A').value);
            double B = double.TryParse(document.getElementById('B').value);
            double C = double.TryParse(document.getElementById('C').value);
            double HRad = double.TryParse(document.getElementById('HRad').value);
            double measInver = double.TryParse(document.getElementById('measInver').value);

            //base heightmap values
            double X = double.TryParse(document.getElementById('X').value) * measInver;
            double Y = double.TryParse(document.getElementById('Y').value) * measInver;
            double Z = double.TryParse(document.getElementById('Z').value) * measInver;
            double XOpp = double.TryParse(document.getElementById('XOpp').value) * measInver;
            double YOpp = double.TryParse(document.getElementById('YOpp').value) * measInver;
            double ZOpp = double.TryParse(document.getElementById('ZOpp').value) * measInver;

            //add one to hrad
            double XTemp1 = double.TryParse(document.getElementById('X1').value) * measInver;
            double YTemp1 = double.TryParse(document.getElementById('Y1').value) * measInver;
            double ZTemp1 = double.TryParse(document.getElementById('Z1').value) * measInver;
            double XOppTemp1 = double.TryParse(document.getElementById('XOpp1').value) * measInver;
            double YOppTemp1 = double.TryParse(document.getElementById('YOpp1').value) * measInver;
            double ZOppTemp1 = double.TryParse(document.getElementById('ZOpp1').value) * measInver;

            double HRadManual = double.TryParse(document.getElementById('HRadManual').value);
            double HRadRatio;
            double newA;
            double newB;
            double newC;
            double newDA;
            double newDB;
            double newDC;
            double newHRad;

            double accuracy = double.TryParse(document.getElementById('accuracy').value);
            double getSteps = double.TryParsedocument.getElementById('stepsPerMM').value);
            double stepsPerMM;

            //XYZ offset
            double xOppPerc;
            double yzPerc;
            double yOppPerc;
            double xzPerc;
            double zOppPerc;
            double xyPerc;

            //diagonal rod
            double deltaTower;
            double deltaOpp;

            //Find the percentages for XYZ Offset
            double offsetX = double.TryParse(document.getElementById('offsetX').value);
            double offsetY = double.TryParse(document.getElementById('offsetY').value);
            double offsetZ = double.TryParse(document.getElementById('offsetZ').value);

            //X offset
            double xOppositePercentage = double.TryParse(document.getElementById('xOppositePercentage').value);
            double yzPercentage = double.TryParse(document.getElementById('yzPercentage').value);
            double X2 = double.TryParse(document.getElementById('X2').value) * measInver;
            double Y2 = double.TryParse(document.getElementById('Y2').value) * measInver;
            double Z2 = double.TryParse(document.getElementById('Z2').value) * measInver;
            double XOpp2 = double.TryParse(document.getElementById('XOpp2').value) * measInver;
            double YOpp2 = double.TryParse(document.getElementById('YOpp2').value) * measInver;
            double ZOpp2 = double.TryParse(document.getElementById('ZOpp2').value) * measInver;

            //Y offset
            double yOppositePercentage = double.TryParse(document.getElementById('yOppositePercentage').value);
            double xzPercentage = double.TryParse(document.getElementById('xzPercentage').value);
            double X3 = double.TryParse(document.getElementById('X3').value) * measInver;
            double Y3 = double.TryParse(document.getElementById('Y3').value) * measInver;
            double Z3 = double.TryParse(document.getElementById('Z3').value) * measInver;
            double XOpp3 = double.TryParse(document.getElementById('XOpp3').value) * measInver;
            double YOpp3 = double.TryParse(document.getElementById('YOpp3').value) * measInver;
            double ZOpp3 = double.TryParse(document.getElementById('ZOpp3').value) * measInver;

            //z offset
            double zOppositePercentage = double.TryParse(document.getElementById('zOppositePercentage').value);
            double xyPercentage = double.TryParse(document.getElementById('xyPercentage').value);
            double X4 = double.TryParse(document.getElementById('X4').value) * measInver;
            double Y4 = double.TryParse(document.getElementById('Y4').value) * measInver;
            double Z4 = double.TryParse(document.getElementById('Z4').value) * measInver;
            double XOpp4 = double.TryParse(document.getElementById('XOpp4').value) * measInver;
            double YOpp4 = double.TryParse(document.getElementById('YOpp4').value) * measInver;
            double ZOpp4 = double.TryParse(document.getElementById('ZOpp4').value) * measInver;

            //find the percentages for diagonal rod offset
            double XTemp5 = double.TryParse(document.getElementById('X5').value) * measInver;
            double YTemp5 = double.TryParse(document.getElementById('Y5').value) * measInver;
            double ZTemp5 = double.TryParse(document.getElementById('Z5').value) * measInver;
            double XOppTemp5 = double.TryParse(document.getElementById('XOpp5').value) * measInver;
            double YOppTemp5 = double.TryParse(document.getElementById('YOpp5').value) * measInver;
            double ZOppTemp5 = double.TryParse(document.getElementById('ZOpp5').value) * measInver;
            double delTower = double.TryParse(document.getElementById('deltaTower').value);
            double delOpp = double.TryParse(document.getElementById('deltaOpp').value);
            double diagonalRod = double.TryParse(document.getElementById('diagonalRod').value);

            //significant figures
            public static double significantFigures(double d, int digits)
            {
                double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);

                return scale * Math.Round(d / scale, digits);
            }

            //check if values are close to zero, then set them to zero - avoids errors
            public double checkZero(double value)
            {
                if (value > 0 && value < accuracy)
                {
                    return 0;
                }
                else if (value < 0 && value > -accuracy)
                {
                    return 0;
                }
                else
                {
                    value = significantFigures(value, 3);
                    return value;
                }
            }
            
            public double getPercentagesOpp(double XYZ, double X, double Y, double Z, double XOpp, double YOpp, double ZOpp, double X2, double Y2, double Z2, double XOpp2, double YOpp2, double ZOpp2) {
                    double percentages;

                    double xDiff = Math.Abs(X2 - X);
                    double yDiff = Math.Abs(Y2 - Y);
                    double zDiff = Math.Abs(Z2 - Z);
                    double xOppDiff = Math.Abs(XOpp2 - XOpp);
                    double yOppDiff = Math.Abs(YOpp2 - YOpp);
                    double zOppDiff = Math.Abs(ZOpp2 - ZOpp);

                    if (XYZ == 'X')
                    {
                        percentages = xOppDiff / xDiff;
                        percentages = checkZero(percentages);
                        return percentages;
                    }
                    else if (XYZ == 'Y')
                    {
                        percentages = yOppDiff / yDiff;
                        percentages = checkZero(percentages);
                        return percentages;
                    }
                    else if (XYZ == 'Z')
                    {
                        percentages = zOppDiff / zDiff;
                        percentages = checkZero(percentages);
                        return percentages;
                    }
                    else
                    {
                        return 0;
                    }
                }

                public double getPercentagesAll(int XYZ, double X, double Y, double Z, double XOpp, double YOpp, double ZOpp, double X2, double Y2, double Z2, double XOpp2, double YOpp2, double ZOpp2) {
                    double percentages;
                    double xDiff = Math.Abs(X2 - X);
                    double yDiff = Math.Abs(Y2 - Y);
                    double zDiff = Math.Abs(Z2 - Z);
                    double xOppDiff = Math.Abs(XOpp2 - XOpp);
                    double yOppDiff = Math.Abs(YOpp2 - YOpp);
                    double zOppDiff = Math.Abs(ZOpp2 - ZOpp);

                    if (XYZ == 1)
                    {
                        percentages = (yDiff + zDiff + yOppDiff + zOppDiff) / (xDiff * 4);
                        percentages = checkZero(percentages);
                        return percentages;
                    }
                    else if (XYZ == 2)
                    {
                        percentages = (xDiff + zDiff + xOppDiff + zOppDiff) / (yDiff * 4);
                        percentages = checkZero(percentages);
                        return percentages;
                    }
                    else if (XYZ == 3)
                    {
                        percentages = (yDiff + xDiff + yOppDiff + xOppDiff) / (zDiff * 4);
                        percentages = checkZero(percentages);
                        return percentages;
                    }
                    else
                    {
                        return 0;             
                    }
                }

            public void calibratePrinter()
            {   
                ////////////////////////////////////////////////////////////////////////////
                //XYZ offset percentages****************************************************
                if (xOppositePercentage > 0)
                {
                    xOppPerc = xOppositePercentage;
                    yzPerc = yzPercentage;

                    yOppPerc = yOppositePercentage;
                    xzPerc = xzPercentage;

                    zOppPerc = zOppositePercentage;
                    xyPerc = xyPercentage;
                }
                else
                {
                    xOppPerc = getPercentagesOpp(1, X, Y, Z, XOpp, YOpp, ZOpp, X2, Y2, Z2, XOpp2, YOpp2, ZOpp2);
                    yzPerc = getPercentagesAll(1, X, Y, Z, XOpp, YOpp, ZOpp, X2, Y2, Z2, XOpp2, YOpp2, ZOpp2);

                    yOppPerc = getPercentagesOpp(2, X, Y, Z, XOpp, YOpp, ZOpp, X3, Y3, Z3, XOpp3, YOpp3, ZOpp3);
                    xzPerc = getPercentagesAll(2, X, Y, Z, XOpp, YOpp, ZOpp, X3, Y3, Z3, XOpp3, YOpp3, ZOpp3);

                    zOppPerc = getPercentagesOpp(3, X, Y, Z, XOpp, YOpp, ZOpp, X4, Y4, Z4, XOpp4, YOpp4, ZOpp4);
                    xyPerc = getPercentagesAll(3, X, Y, Z, XOpp, YOpp, ZOpp, X4, Y4, Z4, XOpp4, YOpp4, ZOpp4);

                    //send data to form
                    document.getElementById('xOppositePercentage').value = xOppPerc;
                    document.getElementById('yzPercentage').value = yzPerc;
                    document.getElementById('yOppositePercentage').value = yOppPerc;
                    document.getElementById('xzPercentage').value = xzPerc;
                    document.getElementById('zOppositePercentage').value = yOppPerc;
                    document.getElementById('xyPercentage').value = xyPerc;
                }

                /////////////////////////////////////////////////////////////////////////////////
                //diagonal rod offset percentages************************************************
                if (delTower > 0 && delOpp > 0)
                {
                    deltaTower = delTower;
                    deltaOpp = delOpp;

                    deltaTower = Math.Abs(deltaTower);
                    deltaOpp = Math.Abs(deltaOpp);
                }
                else
                {
                    deltaTower = ((X - XTemp5) + (Y - YTemp5) + (Z - ZTemp5)) / 3;
                    deltaOpp = ((XOpp - XOppTemp5) + (YOpp - YOppTemp5) + (ZOpp - ZOppTemp5)) / 3;

                    //send data to form
                    document.getElementById('deltaTower').value = deltaTower; // 1/8
                    document.getElementById('deltaOpp').value = deltaOpp; // 1/4

                    deltaTower = Math.Abs(deltaTower);
                    deltaOpp = Math.Abs(deltaOpp);
                }


                //////////////////////////////////////////////////////////////////////////////
                //HRad is calibrated by increasing the outside edge of the glass by the average differences, this should balance the values with a central point of around zero

                if (HRadManual > 0 || HRadManual < 0)
                {
                    HRadRatio = HRadManual;
                }
                else
                {
                    HRadRatio = -(Math.Abs((XTemp1 - X) + (YTemp1 - Y) + (ZTemp1 - Z) + (XOppTemp1 - XOpp) + (YOppTemp1 - YOpp) + (ZOppTemp1 - ZOpp))) / 6;
                    //send data to form
                    document.getElementById('HRadManual').value = HRadRatio;
                }

                double HRadSA = ((X + XOpp + Y + YOpp + Z + ZOpp) / 6);

                HRad = HRad + (HRadSA / HRadRatio);

                X = X - HRadSA;
                Y = Y - HRadSA;
                Z = Z - HRadSA;
                XOpp = XOpp - HRadSA;
                YOpp = YOpp - HRadSA;
                ZOpp = ZOpp - HRadSA;

                X = checkZero(X);
                Y = checkZero(Y);
                Z = checkZero(Z);
                XOpp = checkZero(XOpp);
                YOpp = checkZero(YOpp);
                ZOpp = checkZero(ZOpp);
                

                ////////////////////////////////////////////////////////////////////////////////
                //Tower Offset Calibration******************************************************

                if (getSteps > 0)
                {
                    stepsPerMM = getSteps;
                }
                else
                {
                    //retrieve data
                    double motorStepAngle = document.getElementById('stepSize').value;
                    double driverMicroStepping = document.getElementById('microStepping').value;
                    double beltPitch = document.getElementById('beltPitch').value;
                    double toothCount = document.getElementById('toothCount').value;

                    stepsPerMM = ((360 / motorStepAngle) * driverMicroStepping) / (beltPitch * toothCount);

                    //send data to form
                    document.getElementById('stepsPerMM').value = stepsPerMM;
                }

                //balance axes - retrieve data
                double offsetXYZ = 1 / 0.66;

                int j = 0;
                while (j < 1)
                {
                    double theoryX = offsetX + X * stepsPerMM * offsetXYZ;

                    //correction of one tower allows for XY dimensional accuracy
                    if (X > 0)
                    {
                        //if x is positive
                        offsetX = offsetX + X * stepsPerMM * offsetXYZ;

                        XOpp = XOpp + (X * xOppPerc);//0.5
                        Z = Z + (X * yzPerc);//0.25
                        Y = Y + (X * yzPerc);//0.25
                        ZOpp = ZOpp - (X * yzPerc);//0.25
                        YOpp = YOpp - (X * yzPerc);//0.25
                        X = X - X;
                    }
                    else if (theoryX > 0 && X < 0)
                    {
                        //if x can be decreased
                        offsetX = offsetX + X * stepsPerMM * offsetXYZ;

                        XOpp = XOpp + (X * xOppPerc);//0.5
                        Z = Z + (X * yzPerc);//0.25
                        Y = Y + (X * yzPerc);//0.25
                        ZOpp = ZOpp - (X * yzPerc);//0.25
                        YOpp = YOpp - (X * yzPerc);//0.25
                        X = X - X;
                    }
                    else
                    {
                        //if X is negative
                        offsetY = offsetY - X * stepsPerMM * offsetXYZ;
                        offsetZ = offsetZ - X * stepsPerMM * offsetXYZ;

                        XOpp = XOpp + (X * xOppPerc);
                        Z = Z + (X * yzPerc);
                        Y = Y + (X * yzPerc);
                        ZOpp = ZOpp - (X * yzPerc);
                        YOpp = YOpp - (X * yzPerc);
                        X = X - X;
                    }

                    double theoryY = offsetY + Y * stepsPerMM * offsetXYZ;

                    //Y
                    if (Y > 0)
                    {
                        offsetY = offsetY + Y * stepsPerMM * offsetXYZ;

                        YOpp = YOpp + (Y * yOppPerc);
                        X = X + (Y * xzPerc);
                        Z = Z + (Y * xzPerc);
                        XOpp = XOpp - (Y * xzPerc);
                        ZOpp = ZOpp - (Y * xzPerc);
                        Y = Y - Y;
                    }
                    else if (theoryY > 0 && Y < 0)
                    {
                        offsetY = offsetY + Y * stepsPerMM * offsetXYZ;

                        YOpp = YOpp + (Y * yOppPerc);
                        X = X + (Y * xzPerc);
                        Z = Z + (Y * xzPerc);
                        XOpp = XOpp - (Y * xzPerc);
                        ZOpp = ZOpp - (Y * xzPerc);
                        Y = Y - Y;
                    }
                    else
                    {
                        offsetX = offsetX - Y * stepsPerMM * offsetXYZ;
                        offsetZ = offsetZ - Y * stepsPerMM * offsetXYZ;

                        YOpp = YOpp + (Y * yOppPerc);
                        X = X + (Y * xzPerc);
                        Z = Z + (Y * xzPerc);
                        XOpp = XOpp - (Y * xzPerc);
                        ZOpp = ZOpp - (Y * xzPerc);
                        Y = Y - Y;
                    }

                    double theoryZ = offsetZ + Z * stepsPerMM * offsetXYZ;

                    //Z
                    if (Z > 0)
                    {
                        offsetZ = offsetZ + Z * stepsPerMM * offsetXYZ;

                        ZOpp = ZOpp + (Z * zOppPerc);
                        X = X + (Z * xyPerc);
                        Y = Y + (Z * xyPerc);
                        XOpp = XOpp - (Z * xyPerc);
                        YOpp = YOpp - (Z * xyPerc);
                        Z = Z - Z;
                    }
                    else if (theoryZ > 0 && Z < 0)
                    {
                        offsetZ = offsetZ + Z * stepsPerMM * offsetXYZ;

                        ZOpp = ZOpp + (Z * zOppPerc);
                        X = X + (Z * xyPerc);
                        Y = Y + (Z * xyPerc);
                        XOpp = XOpp - (Z * xyPerc);
                        YOpp = YOpp - (Z * xyPerc);
                        Z = Z - Z;
                    }
                    else
                    {
                        offsetY = offsetY - Z * stepsPerMM * offsetXYZ;
                        offsetX = offsetX - Z * stepsPerMM * offsetXYZ;

                        ZOpp = ZOpp + (Z * zOppPerc);
                        X = X + (Z * xyPerc);
                        Y = Y + (Z * xyPerc);
                        XOpp = XOpp - (Z * xyPerc);
                        YOpp = YOpp - (Z * xyPerc);
                        Z = Z - Z;
                    }

                    X = checkZero(X);
                    Y = checkZero(Y);
                    Z = checkZero(Z);
                    XOpp = checkZero(XOpp);
                    YOpp = checkZero(YOpp);
                    ZOpp = checkZero(ZOpp);

                    if (X < accuracy && X > -accuracy && Y < accuracy && Y > -accuracy && Z < accuracy && Z > -accuracy)
                    {
                        j = 1;
                    }
                }

                //send data back to form
                document.getElementById('offsetX').value = Math.Round(offsetX);
                document.getElementById('offsetY').value = Math.Round(offsetY);
                document.getElementById('offsetZ').value = Math.Round(offsetZ);
    
                ////////////////////////////////////////////////////////////////////////////////
                //Alpha Rotation Calibration****************************************************
                int k = 0;
                while (k < 1)
                {
                    //X Alpha Rotation
                    if (YOpp > ZOpp)
                    {
                        double ZYOppAvg = (YOpp - ZOpp) / 2;
                        A = A + (ZYOppAvg * 1.725); // (0.5/((diff y0 and z0 at X + 0.5)-(diff y0 and z0 at X = 0))) * 2 = 1.75
                        YOpp = YOpp - ZYOppAvg;
                        ZOpp = ZOpp + ZYOppAvg;
                    }
                    else if (YOpp < ZOpp)
                    {
                        double ZYOppAvg = (ZOpp - YOpp) / 2;

                        A = A - (ZYOppAvg * 1.725);
                        YOpp = YOpp + ZYOppAvg;
                        ZOpp = ZOpp - ZYOppAvg;
                    }

                    //Y Alpha Rotation
                    if (ZOpp > XOpp)
                    {
                        double XZOppAvg = (ZOpp - XOpp) / 2;
                        B = B + (XZOppAvg * 1.725);
                        ZOpp = ZOpp - XZOppAvg;
                        XOpp = XOpp + XZOppAvg;
                    }
                    else if (ZOpp < XOpp)
                    {
                        double XZOppAvg = (XOpp - ZOpp) / 2;

                        B = B - (XZOppAvg * 1.725);
                        ZOpp = ZOpp + XZOppAvg;
                        XOpp = XOpp - XZOppAvg;
                    }
                    //Z Alpha Rotation
                    if (XOpp > YOpp)
                    {
                        double YXOppAvg = (XOpp - YOpp) / 2;
                        C = C + (YXOppAvg * 1.725);
                        XOpp = XOpp - YXOppAvg;
                        YOpp = YOpp + YXOppAvg;
                    }
                    else if (XOpp < YOpp)
                    {
                        double YXOppAvg = (YOpp - XOpp) / 2;

                        C = C - (YXOppAvg * 1.725);
                        XOpp = XOpp + YXOppAvg;
                        YOpp = YOpp - YXOppAvg;
                    }
                    //determine if value is close enough
                    double hTow = Math.Max(Math.Max(XOpp, YOpp), ZOpp);
                    double lTow = Math.Min(Math.Min(XOpp, YOpp), ZOpp);
                    double towDiff = hTow - lTow;

                    if (towDiff < accuracy && towDiff > -accuracy)
                    {
                        k = 1;
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////
                //Diagonal Rod Calibration******************************************************
                double diagChange = 1 / deltaOpp;
                double towOppDiff = deltaTower / deltaOpp; //0.5

                int i = 0;
                while (i < 1)
                {
                    double XYZOpp = (XOpp + YOpp + ZOpp) / 3;
                    diagonalRod = diagonalRod + (XYZOpp * diagChange);
                    X = X - towOppDiff * XYZOpp;
                    Y = Y - towOppDiff * XYZOpp;
                    Z = Z - towOppDiff * XYZOpp;
                    XOpp = XOpp - XYZOpp;
                    YOpp = YOpp - XYZOpp;
                    ZOpp = ZOpp - XYZOpp;
                    XYZOpp = (XOpp + YOpp + ZOpp) / 3;
                    XYZOpp = checkZero(XYZOpp);

                    double XYZ = (X + Y + Z) / 3;
                    //hrad
                    HRad = HRad + (XYZ / HRadRatio);

                    if (XYZOpp >= 0)
                    {
                        X = X - XYZ;
                        Y = Y - XYZ;
                        Z = Z - XYZ;
                        XOpp = XOpp - XYZ;
                        YOpp = YOpp - XYZ;
                        ZOpp = ZOpp - XYZ;
                    }
                    else
                    {
                        X = X + XYZ;
                        Y = Y + XYZ;
                        Z = Z + XYZ;
                        XOpp = XOpp + XYZ;
                        YOpp = YOpp + XYZ;
                        ZOpp = ZOpp + XYZ;
                    }

                    X = checkZero(X);
                    Y = checkZero(Y);
                    Z = checkZero(Z);
                    XOpp = checkZero(XOpp);
                    YOpp = checkZero(YOpp);
                    ZOpp = checkZero(ZOpp);

                    //XYZ is zero
                    if (XYZOpp < accuracy && XYZOpp > -accuracy && XYZ < accuracy && XYZ > -accuracy)
                    {
                        i = 1;
                        diagonalRod = checkZero(diagonalRod);
                        //send back to form
                        document.getElementById('diagonalRod').value = diagonalRod;
                    }
                }

                //send obtained values back to the form*************************************
                document.getElementById('A').value = checkZero(A);
                document.getElementById('B').value = checkZero(B);
                document.getElementById('C').value = checkZero(C);
                document.getElementById('HRad').value = checkZero(HRad);
                document.getElementById('HRadManual').value = HRadRatio;
            }
        }
    }
}
