using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDM.AdcGateway
{
    public partial class FormMain : Form
    {
        private readonly IPartyCrossMapService _partyCrossMapService;
        private IPartyCrossMapFactory _partyCrossMapFactory;

        public FormMain()
        {
            InitializeComponent();

            //todo pull off container
            _partyCrossMapService = new PartyCrossMapService();
            _partyCrossMapFactory = new PartyCrossMapFactory();
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            var data = _partyCrossMapService.FetchData();

            var mappingCounterparties = _partyCrossMapFactory.CreateMappingCounterparties(data);

            var s = "";


        }
    }
}
