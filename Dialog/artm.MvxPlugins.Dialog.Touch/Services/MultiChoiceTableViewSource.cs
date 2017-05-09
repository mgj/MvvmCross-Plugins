
using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;

namespace artm.MvxPlugins.Dialog.Touch.Services
{
    public class MultiChoiceTableViewSource : MvxStandardTableViewSource
    {
        private readonly bool[] _checkedItems;

        public MultiChoiceTableViewSource(UITableView table, bool[] checkedItems) : base(table)
            {
            // Make copy of array in order to be able to return the original checkedItems array (for the cancel button)
            _checkedItems = (bool[])checkedItems.Clone();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = base.GetCell(tableView, indexPath);

            //cell.ImageView.Image = UIImage.FromFile("Images/" + tableItems[indexPath.Row].ImageName); // don't use for Value2


            cell.SelectionStyle = UITableViewCellSelectionStyle.None;

            var hero = _checkedItems[indexPath.Row];
            if (hero == true)
            {
                cell.Accessory = UITableViewCellAccessory.Checkmark;
            }
            else
            {
                cell.Accessory = UITableViewCellAccessory.None;
            }

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            _checkedItems[indexPath.Row] = true;

            var cell = tableView.CellAt(indexPath);
            if (cell == null) return;
            cell.Accessory = UITableViewCellAccessory.Checkmark;
        }

        public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
        {
            _checkedItems[indexPath.Row] = false;
            var cell = tableView.CellAt(indexPath);
            if (cell == null) return;
            cell.Accessory = UITableViewCellAccessory.None;
        }
    }
}