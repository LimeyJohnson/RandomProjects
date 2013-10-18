using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FriendsFlockInterface.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace FriendsFlockInterface
{
    public class CanvasModel : ModelBase
    {
        private FriendsGraph friendsGraph;

        private Canvas ZoomPanCanvas;
        private Image EdgeMapImage;
        private Border LoadingProgress;
        private Border Title;

        private Dictionary<long, VertexControl> vertexControls = new Dictionary<long, VertexControl>();
        private List<FriendsVertex> vertexMutualFriends = new List<FriendsVertex>();
        private List<FriendsVertex> vertexShorestPath = new List<FriendsVertex>();
        private List<Line> edgeMutualFriendsControls = new List<Line>();
        private List<Line> edgeShorestPathControls = new List<Line>();
        private bool isFlockLayout = false;

        public CanvasModel(FriendsGraph friendsGraph)
        {
            this.friendsGraph = friendsGraph;

            Messenger.Default.Register<Canvas>(this, SaveCanvas);
            Messenger.Default.Register<Image>(this, SaveImage);
            Messenger.Default.Register<CurrentFriends>(this, UpdateControls);
            Messenger.Default.Register<Border>(this, "brdLoadingProgress", SaveLoadingProgress);
            Messenger.Default.Register<Border>(this, "brdTitle", SaveTitle);
        }

        #region Save Pointers
        private void SaveCanvas(Canvas ZoomPanCanvas)
        {
            this.ZoomPanCanvas = ZoomPanCanvas;
        }
        private void SaveImage(Image EdgeMapImage)
        {
            this.EdgeMapImage = EdgeMapImage;
        }
        private void SaveLoadingProgress(Border LoadingProgress)
        {
            this.LoadingProgress = LoadingProgress;
        }
        private void SaveTitle(Border Title)
        {
            this.Title = Title;
        }
        #endregion

        #region SyncGridControls, SyncFlockControls
        public void SyncGridControls(AppModel appModel)
        {
            Debug.WriteLine("SyncGridControls");
            GalaSoft.MvvmLight.Threading.DispatcherHelper.UIDispatcher.BeginInvoke(() => 
                {
                    //Grid Layout
                    lock (friendsGraph)
                    {
                        friendsGraph.ComputeGridLayout();
                    }

                    //Cache Friends' Updates
                    CacheUpdates cu = new CacheUpdates(friendsGraph);
                    cu.Run();

                    //Create Vertex Controls
                    foreach (FriendsVertex fv in friendsGraph.VertexDictionary.Values)
                    {
                        //Create Vertex Control, Set Picture
                        VertexControl vc = new VertexControl(fv, appModel);
                        if (!string.IsNullOrEmpty(fv.Info.Pic_Sqaure_Url))
                            vc.PicSource = new BitmapImage(new Uri(fv.Info.Pic_Sqaure_Url, UriKind.RelativeOrAbsolute));
                        
                        //Cache to Dictionary
                        vertexControls.Add(vc.Friend.Uid, vc);

                        //Add to Canvas
                        ZoomPanCanvas.Children.Add(vc);
                    }
                });
        }

        public void SyncFlockControls()
        {
            Debug.WriteLine("SyncFlockControls");
            GalaSoft.MvvmLight.Threading.DispatcherHelper.UIDispatcher.BeginInvoke(() =>
                {
                    //Get Max Values
                    double maxX = 0d;
                    double maxY = 0d;
                    foreach (FriendsVertex v in friendsGraph.VertexDictionary.Values)
                    {
                        if (maxX < v.Layout.FlockPoint.X)
                            maxX = v.Layout.FlockPoint.X;

                        if (maxY < v.Layout.FlockPoint.Y)
                            maxY = v.Layout.FlockPoint.Y;
                    }
                    maxX += 60d;
                    maxY += 60d;

                    friendsGraph.EdgeMapBitmap = new WriteableBitmap(Convert.ToInt32(maxX), Convert.ToInt32(maxY));
                    WriteableBitmap wb = friendsGraph.EdgeMapBitmap;

                    //Draw Lines
                    int centerOffsent = 28;
                    foreach (FriendsEdge fes in friendsGraph.EdgeList)
                    {
                        int x1 = Convert.ToInt32(fes.Source.Layout.FlockPoint.X) + centerOffsent;
                        int y1 = Convert.ToInt32(fes.Source.Layout.FlockPoint.Y) + centerOffsent;
                        int x2 = Convert.ToInt32(fes.Target.Layout.FlockPoint.X) + centerOffsent;
                        int y2 = Convert.ToInt32(fes.Target.Layout.FlockPoint.Y) + centerOffsent;
                        wb.DrawLineDDA(x1, y1, x2, y2, Colors.White);
                    }
                    
                    EdgeMapImage.Source = friendsGraph.EdgeMapBitmap;   //Save EdgeMap
                    EdgeMapImage.Visibility = Visibility.Collapsed;     //Default is Collapsed (Grid Layout) TODO
                    LoadingProgress.Visibility = Visibility.Collapsed;  //Hide Loading Progress
                    Title.Opacity = 100;                                //Show Title
                });
        }
        #endregion

        #region Dynamic Lines
        private void UpdateControls(CurrentFriends currentFriends)
        {
            UpdateMutualFriendsControls(currentFriends.SelectedFriend);
            UpdateShorestPathControls(currentFriends.SelectedFriend, currentFriends.PathTarget);
        }
        private void UpdateMutualFriendsControls(FriendsVertex CurrentSelectedFriend)
        {
            if (CurrentSelectedFriend.Uid == 0d)
                return;

            //Clear
            foreach (FriendsVertex fv in vertexMutualFriends)
            {
                fv.State.IsMutualFriend = false;
            }

            //Clear
            foreach (Line L in edgeMutualFriendsControls)
            {
                ZoomPanCanvas.Children.Remove(L);
            }

            long currentUid = CurrentSelectedFriend.Uid;
            foreach (FriendsVertex v in friendsGraph.VertexDictionary[currentUid].AdjacentVertexes.Values)
            {
                vertexMutualFriends.Add(v);
                v.State.IsMutualFriend = true;

                int vertexCenter = 28;

                Line L = new Line();
                L.Fill = new SolidColorBrush(Colors.Blue);
                L.Stroke = new SolidColorBrush(Colors.Blue);
                L.StrokeThickness = 4d;
                L.X1 = CurrentSelectedFriend.Layout.FlockPoint.X + vertexCenter;
                L.Y1 = CurrentSelectedFriend.Layout.FlockPoint.Y + vertexCenter;
                L.X2 = v.Layout.FlockPoint.X + vertexCenter;
                L.Y2 = v.Layout.FlockPoint.Y + vertexCenter;
                L.SetValue(Canvas.ZIndexProperty, 1);

                if (isFlockLayout)
                    L.Visibility = Visibility.Visible;
                else
                    L.Visibility = Visibility.Collapsed;

                edgeMutualFriendsControls.Add(L);
                ZoomPanCanvas.Children.Add(L);
            }

        }
        private void UpdateShorestPathControls(FriendsVertex CurrentSelectedFriend, FriendsVertex CurrentShorestPathTarget)
        {
            if (CurrentSelectedFriend.Uid == 0d)
                return;

            if (CurrentShorestPathTarget.Uid == 0d)
                return;

            //Clear
            foreach (FriendsVertex fv in vertexShorestPath)
            {
                fv.State.IsShorestPathMember = false;
                fv.State.IsShorestPathTarget = false;
            }

            //Clear
            foreach (Line L in edgeShorestPathControls)
            {
                ZoomPanCanvas.Children.Remove(L);
            }

            List<FriendsEdge> shorestPath = friendsGraph.GetShorestPath(CurrentSelectedFriend, CurrentShorestPathTarget);

            foreach (FriendsEdge edge in shorestPath)
            {
                vertexShorestPath.Add(edge.Source);
                vertexShorestPath.Add(edge.Target);
                edge.Source.State.IsShorestPathMember = true;
                edge.Target.State.IsShorestPathTarget = true;

                int vertexCenter = 28;

                Line L = new Line();
                L.Fill = new SolidColorBrush(Colors.Red);
                L.Stroke = new SolidColorBrush(Colors.Red);
                L.StrokeThickness = 4d;
                L.X1 = edge.Source.Layout.FlockPoint.X + vertexCenter;
                L.Y1 = edge.Source.Layout.FlockPoint.Y + vertexCenter;
                L.X2 = edge.Target.Layout.FlockPoint.X + vertexCenter;
                L.Y2 = edge.Target.Layout.FlockPoint.Y + vertexCenter;
                L.SetValue(Canvas.ZIndexProperty, 2);
                
                if (isFlockLayout)
                    L.Visibility = Visibility.Visible;
                else
                    L.Visibility = Visibility.Collapsed;


                edgeShorestPathControls.Add(L);
                ZoomPanCanvas.Children.Add(L);
            }
        }
        #endregion

        #region Layout
        public void GotoGridLayout()
        {
            isFlockLayout = false;

            if (EdgeMapImage != null)
                EdgeMapImage.Visibility = Visibility.Collapsed;

            foreach (Line edgeLine in edgeMutualFriendsControls)
            {
                edgeLine.Visibility = Visibility.Collapsed;
            }

            foreach (Line edgeLine in edgeShorestPathControls)
            {
                edgeLine.Visibility = Visibility.Collapsed;
            }

            foreach (VertexControl vc in vertexControls.Values)
            {
                vc.Friend.Layout.GotoGridLayout();
                vc.RefreshState();
            }
        }

        public void GotoFlockLayout()
        {
            GalaSoft.MvvmLight.Threading.DispatcherHelper.UIDispatcher.BeginInvoke(() =>
                {
                    isFlockLayout = true;

                    if (EdgeMapImage != null)
                        EdgeMapImage.Visibility = Visibility.Visible;

                    foreach (Line edgeLine in edgeMutualFriendsControls)
                    {
                        edgeLine.Visibility = Visibility.Visible;
                    }

                    foreach (Line edgeLine in edgeShorestPathControls)
                    {
                        edgeLine.Visibility = Visibility.Visible;
                    }

                    foreach (VertexControl vc in vertexControls.Values)
                    {
                        vc.Friend.Layout.GotoFlockLayout();
                        vc.RefreshState();
                    }
                });
        }
        #endregion

        public void HighlightUpdates(HighlightUpdatesEnum HighlightUpdates)
        {
            foreach (FriendsVertex fv in friendsGraph.VertexDictionary.Values)
            {
                fv.State.IsHighlighted = false;

                switch (HighlightUpdates)
                {
                    case HighlightUpdatesEnum.Profile:
                        if (fv.State.ProfileUpdated)
                            fv.State.IsHighlighted = true;
                        break;
                    case HighlightUpdatesEnum.Status:
                        if (fv.State.StatusUpdated)
                            fv.State.IsHighlighted = true;
                        break;
                    case HighlightUpdatesEnum.Birthday:
                        if (fv.State.BirthdayUpdated)
                            fv.State.IsHighlighted = true;
                        break;
                    case HighlightUpdatesEnum.Relationshiop:
                        if (fv.State.Relationship)
                            fv.State.IsHighlighted = true;
                        break;
                    case HighlightUpdatesEnum.Clear:
                        break;
                }
            }
        }
    }

    public enum HighlightUpdatesEnum { Profile, Status, Birthday, Relationshiop, Clear }
}
