using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using HeThongPOS.WPF.ViewModels;

namespace HeThongPOS.WPF.Views;

public partial class ReportsView : UserControl
{
    private ReportsViewModel? _viewModel;

    public ReportsView()
    {
        InitializeComponent();
        this.DataContextChanged += ReportsView_DataContextChanged;
        this.Loaded += ReportsView_Loaded;
    }

    private void ReportsView_DataContextChanged(object? sender, DependencyPropertyChangedEventArgs e)
    {
        if (_viewModel != null)
        {
            _viewModel.ChartPoints.CollectionChanged -= ChartPoints_CollectionChanged;
            _viewModel.ChartOrderPoints.CollectionChanged -= ChartPoints_CollectionChanged;
        }

        _viewModel = e.NewValue as ReportsViewModel;

        if (_viewModel != null)
        {
            _viewModel.ChartPoints.CollectionChanged += ChartPoints_CollectionChanged;
            _viewModel.ChartOrderPoints.CollectionChanged += ChartPoints_CollectionChanged;
            DrawChart();
        }
    }

    private void ReportsView_Loaded(object? sender, RoutedEventArgs e)
    {
        DrawChart();
    }

    private void ChartPoints_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        DrawChart();
    }

    private void DrawChart()
    {
        if (_viewModel == null || !IsLoaded) return;

        ChartCanvas.Children.Clear();

        // Canvas size
        double canvasHeight = ChartCanvas.ActualHeight > 0 ? ChartCanvas.ActualHeight : 160;
        double canvasWidth = ChartCanvas.ActualWidth > 0 ? ChartCanvas.ActualWidth : 480;

        // Draw horizontal grid lines (e.g., 3 lines)
        for (int i = 0; i <= 3; i++)
        {
            double y = 40 + i * 33.3; // between 40 and 140
            var line = new Line
            {
                X1 = 0,
                Y1 = y,
                X2 = canvasWidth,
                Y2 = y,
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F1F5F9")),
                StrokeThickness = 1
            };
            ChartCanvas.Children.Add(line);
        }

        // Draw Revenue Trend Line (Solid Greenish #4C8A64 matching PrimaryColor)
        if (_viewModel.ChartPoints.Count > 0)
        {
            var polyline = new Polyline
            {
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C8A64")),
                StrokeThickness = 3,
                StrokeLineJoin = PenLineJoin.Round
            };

            var points = new PointCollection();
            foreach (var pt in _viewModel.ChartPoints)
            {
                points.Add(pt);
            }
            polyline.Points = points;
            ChartCanvas.Children.Add(polyline);

            // Draw area under curve (premium looking gradient/fill!)
            if (points.Count > 0)
            {
                var areaPolygon = new Polygon
                {
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C8A64")) { Opacity = 0.12 },
                    StrokeThickness = 0
                };
                var areaPoints = new PointCollection();
                areaPoints.Add(new Point(points[0].X, 150)); // bottom left
                foreach (var pt in points)
                {
                    areaPoints.Add(pt);
                }
                areaPoints.Add(new Point(points[points.Count - 1].X, 150)); // bottom right
                areaPolygon.Points = areaPoints;
                ChartCanvas.Children.Add(areaPolygon);
            }

            // Draw Ellipses at each point
            foreach (var pt in points)
            {
                var ellipse = new Ellipse
                {
                    Width = 8,
                    Height = 8,
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C8A64")),
                    Stroke = Brushes.White,
                    StrokeThickness = 1.5
                };
                Canvas.SetLeft(ellipse, pt.X - 4);
                Canvas.SetTop(ellipse, pt.Y - 4);
                ChartCanvas.Children.Add(ellipse);
            }
        }

        // Draw Order Count Trend Line (Dashed Orangeish #F59E0B)
        if (_viewModel.ChartOrderPoints.Count > 0)
        {
            var polyline = new Polyline
            {
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F59E0B")),
                StrokeThickness = 1.5,
                StrokeDashArray = new DoubleCollection(new double[] { 4, 3 }),
                StrokeLineJoin = PenLineJoin.Round
            };

            var points = new PointCollection();
            foreach (var pt in _viewModel.ChartOrderPoints)
            {
                points.Add(pt);
            }
            polyline.Points = points;
            ChartCanvas.Children.Add(polyline);

            // Draw Ellipses at each point
            foreach (var pt in points)
            {
                var ellipse = new Ellipse
                {
                    Width = 6,
                    Height = 6,
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F59E0B")),
                    Stroke = Brushes.White,
                    StrokeThickness = 1
                };
                Canvas.SetLeft(ellipse, pt.X - 3);
                Canvas.SetTop(ellipse, pt.Y - 3);
                ChartCanvas.Children.Add(ellipse);
            }
        }
    }
}
