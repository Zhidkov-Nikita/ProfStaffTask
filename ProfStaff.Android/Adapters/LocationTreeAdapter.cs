using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using ProfStaff.Models;
using ProfStaff.Droid;

public class LocationTreeAdapter : RecyclerView.Adapter
{
    private readonly List<Location> _displayedLocations;
    private readonly List<Location> _allLocations;

    public LocationTreeAdapter(List<Location> locations)
    {
        _allLocations = locations;
        _displayedLocations = new List<Location>();
        BuildTree();
        FlattenTree(_allLocations, _displayedLocations, 0);
    }

    private void BuildTree()
    {
        var lookup = _allLocations.ToDictionary(x => x.Id);
        foreach (var location in _allLocations)
        {
            if (location.Parent_ID.HasValue && lookup.ContainsKey(location.Parent_ID.Value))
            {
                lookup[location.Parent_ID.Value].Children.Add(location);
            }
        }
    }

    private void FlattenTree(List<Location> nodes, List<Location> result, int level)
    {
        foreach (var node in nodes)
        {
            node.Level = level;
            result.Add(node);

            if (node.IsExpanded && node.Children.Count > 0)
            {
                FlattenTree(node.Children, result, level + 1);
            }
        }
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        var itemView = LayoutInflater.From(parent.Context)
            .Inflate(Resource.Layout.location_item, parent, false);
        return new LocationViewHolder(itemView, OnItemClick);
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
        var viewHolder = holder as LocationViewHolder;
        var location = _displayedLocations[position];

        // Установка отступа по уровню
        viewHolder.ItemView.SetPadding(50 * location.Level, 0, 0, 0);
        viewHolder.Name.Text = location.Name;

        // Настройка иконки раскрытия
        if (location.Children.Count > 0)
        {
            viewHolder.ExpandIcon.Visibility = ViewStates.Visible;
            viewHolder.ExpandIcon.SetImageResource(
                location.IsExpanded ? Resource.Drawable.mtrl_ic_arrow_drop_up : Resource.Drawable.mtrl_ic_arrow_drop_down);
        }
        else
        {
            viewHolder.ExpandIcon.Visibility = ViewStates.Invisible;
        }
    }

    private void OnItemClick(int position)
    {
        var location = _displayedLocations[position];
        if (location.Children.Count > 0)
        {
            location.IsExpanded = !location.IsExpanded;
            _displayedLocations.Clear();
            FlattenTree(_allLocations, _displayedLocations, 0);
            NotifyDataSetChanged();
        }
    }

    public override int ItemCount => _displayedLocations.Count;
}

public class LocationViewHolder : RecyclerView.ViewHolder
{
    public TextView Name { get; }
    public ImageView ExpandIcon { get; }

    public LocationViewHolder(View itemView, Action<int> listener) : base(itemView)
    {
        Name = itemView.FindViewById<TextView>(Resource.Id.location_name);
        ExpandIcon = itemView.FindViewById<ImageView>(Resource.Id.expand_icon);

        itemView.Click += (sender, e) => listener(base.LayoutPosition);
    }
}