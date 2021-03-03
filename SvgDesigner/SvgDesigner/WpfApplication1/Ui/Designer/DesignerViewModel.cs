using Database.DataRepository;
using GeometryModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using WpfApplication1.ShapeModel;
using WpfApplication1.Utility;

namespace WpfApplication1.Ui.Designer
{
    public class DesignerViewModel : ViewModelBase
    {
        public double CanvasWidth { get; set; }
        public double CanvasHeight { get; set; }

        public ObservableCollection<Shp> ObjList { get; set; }

        private int _selectedItem;
        public int SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; RaisePropertyChanged(nameof(SelectedItem)); }
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(OnAddExecute, () => true)); }
        }
        private void OnAddExecute()
        {
            ObjList.Add(new ObjMy() { Id = 9, X = 210, Y = 30, Width = 10, Height = 10, TypeId = 2 });
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

        public RelayCommand<object> OnMouseDoubleClickCmd { get; }
        private void OnMouseDoubleClickCmdExecute(object obj)
        {
            int id = 0;
            MouseEventArgs e = (MouseEventArgs)obj;
            var position = e.GetPosition(e.Device.Target);
            if (e.Device.Target is Line)
            {
                id = Convert.ToInt32(((Line)e.Device.Target).Tag);
            }
            if (e.Device.Target is Ellipse)
            {
                id = Convert.ToInt32(((Ellipse)e.Device.Target).Tag);
            }
            if (e.Device.Target is Rectangle)
            {
                id = Convert.ToInt32(((Rectangle)e.Device.Target).Tag);

            }
            SelectedItem = id;
            var shp = ObjList.FirstOrDefault(x => x.Id == id);
            Messenger.Default.Send(shp);
        }

        public DesignerViewModel()
        {
            //ObjList = _objTempList;

            OnMouseDoubleClickCmd = new RelayCommand<object>(OnMouseDoubleClickCmdExecute);

            double svgWidth = 800;
            double svgHeight = 800;
            double margin = 20;

            double dotR = 0.5;

            CanvasWidth = svgWidth + 2 * margin;
            CanvasHeight = svgHeight + 2 * margin;

            var pointTopLeft = MainRepo.GetPointTopLeft();
            var pointBottomRight = MainRepo.GetPointBottomRight();
            var xFactor = svgWidth / (pointBottomRight.X - pointTopLeft.X);
            var yFactor = svgHeight / (pointBottomRight.Y - pointTopLeft.Y);


            var pipeList = MainRepo.GetPipeList();
            pipeList.ForEach(t => t.Geometry.ForEach(p => { p.X = (p.X - pointTopLeft.X) * xFactor + margin; p.Y = (pointBottomRight.Y - p.Y) * yFactor + margin; }));
            var linkMyList = pipeList.Select(o => new LinkMy
            {
                Id = o.ID,
                Name = o.Label,
                X = o.Geometry[0].X,
                Y = o.Geometry[0].Y,

                X2 = o.Geometry.Last().X - o.Geometry[0].X,
                Y2 = o.Geometry.Last().Y - o.Geometry[0].Y,

                Path = o.Geometry,
                TypeId = 6,
            });

            //var junctionList = MainRepo.GetJunctionRecalcList(2000, 1000);
            var junctionList = MainRepo.GetJunctionList();
            junctionList.ForEach(p => { p.Geometry[0].X = (p.Geometry[0].X - pointTopLeft.X) * xFactor + margin; p.Geometry[0].Y = (pointBottomRight.Y - p.Geometry[0].Y) * yFactor + margin; });
            var objMyList = junctionList.Select(j => new ObjMy
            {
                Id = j.ID,
                X = j.Geometry[0].X - dotR,
                Y = j.Geometry[0].Y - dotR,
                Width = 2 * dotR,
                Height = 2 * dotR,
                TypeId = 2
            });

            var customerNodeList = MainRepo.GetCustomerNodeList();
            customerNodeList.ForEach(p => { p.Geometry[0].X = (p.Geometry[0].X - pointTopLeft.X) * xFactor + margin; p.Geometry[0].Y = (pointBottomRight.Y - p.Geometry[0].Y) * yFactor + margin; });
            var cnShpList = customerNodeList.Select(j => new CnShp
            {
                Id = j.ID,
                X = j.Geometry[0].X - dotR,
                Y = j.Geometry[0].Y - dotR,
                Width = 2 * dotR,
                Height = 2 * dotR,
                TypeId = 7
            });

            ObjList = new ObservableCollection<Shp>(
                linkMyList.Select(l => (Shp)l)
                    .Union(objMyList.Select(o => (Shp)o))
                    .Union(cnShpList.Select(c => (Shp)c))
                );
        }

        #region Waste
        private readonly ObservableCollection<Shp> _objTempList = new ObservableCollection<Shp>()
        {
            //new ObjMy() {Id=1, X=10,  Y=30,  Width=15,  Height=15, TypeId=2 },
            //new ObjMy() {Id=2, X=70,  Y=40,  Width=15,  Height=15, TypeId=2 },
            //new ObjMy() {Id=3, X=20,  Y=80,  Width=15,  Height=15, TypeId=2 },
            //new ObjMy() {Id=4, X=80,  Y=100, Width=15,  Height=15, TypeId=2 },
            //new ObjMy() {Id=5, X=110, Y=30,  Width=15,  Height=15, TypeId=2 },
            //new ObjMy() {Id=6, X=170, Y=40,  Width=15,  Height=15, TypeId=2 },
            //new ObjMy() {Id=7, X=120, Y=80,  Width=15,  Height=15, TypeId=2 },
            //new ObjMy() {Id=8, X=180, Y=100, Width=15,  Height=15, TypeId=2 },

            new ObjMy() {Id=1, TypeId=2, X=150,  Y=50,   Width=15,  Height=15 },
            new ObjMy() {Id=2, TypeId=2, X=100,  Y=150,  Width=15,  Height=15 },

            new LinkMy() {Id=11, TypeId=6, X=210,  Y=30,  X2=10,   Y2=50  },
            new LinkMy() {Id=12, TypeId=6, X=150,  Y=50,  X2=-50,  Y2=100 },
        };
        #endregion
    }
}
