﻿using Database.DataRepository;
using Grundfos.GeometryModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApplication1.ShapeModel;
using WpfApplication1.Utility;

namespace WpfApplication1
{
    public class DataRepo
    {
        private readonly ObservableCollection<Shp> _objTempList = new ObservableCollection<Shp>()
        {
            //new ObjMy() {ObjId=1, X=10,  Y=30,  Width=15,  Height=15, TypeId=6 },
            //new ObjMy() {ObjId=2, X=70,  Y=40,  Width=15,  Height=15, TypeId=6 },
            //new ObjMy() {ObjId=3, X=20,  Y=80,  Width=15,  Height=15, TypeId=6 },
            //new ObjMy() {ObjId=4, X=80,  Y=100, Width=15,  Height=15, TypeId=6 },
            //new ObjMy() {ObjId=5, X=110, Y=30,  Width=15,  Height=15, TypeId=2 },
            //new ObjMy() {ObjId=6, X=170, Y=40,  Width=15,  Height=15, TypeId=2 },
            //new ObjMy() {ObjId=7, X=120, Y=80,  Width=15,  Height=15, TypeId=2 },
            //new ObjMy() {ObjId=8, X=180, Y=100, Width=15,  Height=15, TypeId=2 },

            new ObjMy() {ObjId=1, X=150,  Y=50,  Width=15,  Height=15, TypeId=6 },
            new ObjMy() {ObjId=2, X=100,  Y=150,  Width=15,  Height=15, TypeId=2 },

            //new LinkMy() {LinkId=11, X=210,  Y=30,  X2=10,  Y2=50, TypeId=6 },
            new LinkMy() {LinkId=12, X=150,  Y=50,  X2=-50,  Y2=100, TypeId=6 },
        };


        public double CanvasWidth { get; set; }  
        public double CanvasHeight { get; set; }





        public ObservableCollection<Shp> ObjList { get; set; }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(OnAddExecute, ()=>true)); }
        }
        private void OnAddExecute()
        {
            ObjList.Add(new ObjMy() { ObjId = 9, X = 210, Y = 30, Width = 10, Height = 10, TypeId = 2 });
        }

        private ICommand _moveCommand;
        public ICommand MoveCommand
        {
            get { return _moveCommand ?? (_moveCommand = new RelayCommand(OnMoveExecute, () => true)); }
        }
        private void OnMoveExecute()
        {
            var obj = ObjList.LastOrDefault();
            if (obj != null) obj.X += 20;
        }

        public DataRepo()
        {
            double svgWidth = 2000;
            double svgHeight = 2000;
            double margin = 20;

            CanvasWidth = svgWidth + 2 * margin;
            CanvasHeight = svgHeight + 2 * margin;


            //ObjList = _objTempList;


            //var junctionList = MainRepo.GetJunctionRecalcList(2000, 1000)
            //    //.Take(1000)
            //    ;
            //var objMyList = junctionList.Select(j => new ObjMy { 
            //    ObjId = (uint)j.ID, 
            //    X = j.Center.X, 
            //    Y = j.Center.Y, 
            //    Width = 4, 
            //    Height = 4, 
            //    TypeId = 2 
            //});

            //ObjList = new ObservableCollection<Shp>(objMyList);


            var pipeList = GetPipeRecalcList(svgWidth, svgHeight, margin)
                //.Take(1000)
                ;
            var linkMyList = pipeList.Select(o => new LinkMy
            {
                LinkId = (uint)o.ID,
                X = o.Path[0].X,
                Y = o.Path[0].Y,
                X2 = o.Path.Last().X - o.Path[0].X,
                Y2 = o.Path.Last().Y - o.Path[0].Y,
                TypeId = 6,
            });

            ObjList = new ObservableCollection<Shp>(linkMyList);
        }

        public static List<Pipe> GetPipeRecalcList(double width, double height, double margin)
        {
            var pointTopLeft = MainRepo.GetPointTopLeft();
            var pointBottomRight = MainRepo.GetPointBottomRight();
            var xFactor = width / (pointBottomRight.X - pointTopLeft.X);
            var yFactor = height / (pointBottomRight.Y - pointTopLeft.Y);

            var list = MainRepo.GetPipeList();
            //list.ForEach(t => t.Path.ToList().ForEach(p => { p.X = (p.X - pointTopLeft.X) * xFactor; p.Y = (pointBottomRight.Y - p.Y) * yFactor; }));
            list.ForEach(t => t.Path.ToList().ForEach(p => { p.X = (p.X - pointTopLeft.X) * xFactor + margin; p.Y = (pointBottomRight.Y - p.Y) * yFactor + margin; }));
            return list;
        }



    }
}
