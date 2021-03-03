using Database.DataRepository;
using GeometryModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Utility;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WpfApplication1.Ui.Designer.Pipe
{
    public class ItemViewModel : ViewModelBase
    {

        private int _id;
        [Category("General")]
        [DisplayName("ID")]
        public int Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
        }

        private string _name;
        [Category("General")]
        [DisplayName("Label")]
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        private List<Point2D> _path;
        [Category("Geometry")]
        [DisplayName("Geometry")]
        public List<Point2D> Path
        {
            get { return _path; }
            set { _path = value; RaisePropertyChanged("Path"); }
        }




        //public double HMITopologyStartNodeID { get; set; }

        [Category("Topology")]
        [DisplayName("Start Node")]
        public string HMITopologyStartNodeLabel { get; set; }

        //public double HMITopologyStopNodeID { get; set; }

        [Category("Topology")]
        [DisplayName("Stop Node")]
        public string HMITopologyStopNodeLabel { get; set; }

        [Category("Physical")]
        [DisplayName("User Defined Length")]
        public bool Physical_IsUserDefinedLength { get; set; }

        [Category("Physical")]
        [DisplayName("Pipe Status")]
        public int PipeStatus { get; set; }

        [Category("Physical")]
        [DisplayName("Pipe Material")]
        public string Physical_PipeMaterial { get; set; }

        [Category("Physical")]
        [DisplayName("Installation Year")]
        public int Physical_InstallationYear { get; set; }

        [Category("Physical")]
        [DisplayName("HMI Geometry Scaled Length")]
        public double HMIGeometryScaledLength { get; set; }

        [Category("Physical")]
        [DisplayName("Pipe Diameter")]
        public double Physical_PipeDiameter { get; set; }



        public ItemViewModel(int id)
        {
            var model = MainRepo.GetPipeList().FirstOrDefault(x => x.ID == id);

            Id = model.ID;
            Name = model.Label;
            //General = new GeneralProps()
            //{
            //    Id = model.ID,
            //    Name = model.Label,
            //};
            Path = model.Geometry.ToList();

            HMITopologyStartNodeLabel = (string)model.Fields["HMITopologyStartNodeLabel"];
            HMITopologyStopNodeLabel = (string)model.Fields["HMITopologyStopNodeLabel"];
            Physical_IsUserDefinedLength = (bool)model.Fields["Physical_IsUserDefinedLength"];
            PipeStatus = (int)model.Fields["PipeStatus"];
            Physical_PipeMaterial = (string)model.Fields["Physical_PipeMaterial"];
            Physical_InstallationYear = (int)model.Fields["Physical_InstallationYear"];
            HMIGeometryScaledLength = (double)model.Fields["HMIGeometryScaledLength"];
            Physical_PipeDiameter = (double)model.Fields["Physical_PipeDiameter"];
        }
    }

    //public class GeneralProps : ViewModelBase
    //{
    //    private int _id;
    //    //[Category("General")]
    //    [DisplayName("ID")]
    //    public int Id
    //    {
    //        get { return _id; }
    //        set { _id = value; RaisePropertyChanged("Id"); }
    //    }
    //    private string _name;
    //    //[Category("General")]
    //    [DisplayName("Label")]
    //    public string Name
    //    {
    //        get { return _name; }
    //        set { _name = value; RaisePropertyChanged("Name"); }
    //    }
    //}
}
