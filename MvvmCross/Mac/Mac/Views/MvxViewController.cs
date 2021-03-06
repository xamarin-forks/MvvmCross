﻿using System;
using AppKit;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Mac.Views;

namespace MvvmCross.Mac.Views
{
    public class MvxViewController
        : MvxEventSourceViewController
            , IMvxMacView
    {
        // Called when created from unmanaged code
        public MvxViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        public MvxViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public MvxViewController(string viewName, NSBundle bundle) : base(viewName, bundle)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public MvxViewController(string viewName) : base(viewName, NSBundle.MainBundle)
        {
            Initialize();
        }

        public MvxViewController() : base()
        {
            Initialize();
        }

        // Shared initialization code
        private void Initialize()
        {
            this.AdaptForBinding();
        }

        public object DataContext
        {
            get { return BindingContext.DataContext; }
            set { BindingContext.DataContext = value; }
        }

        public IMvxViewModel ViewModel
        {
            get { return (IMvxViewModel)DataContext; }
            set { DataContext = value; }
        }

        public MvxViewModelRequest Request { get; set; }

        public IMvxBindingContext BindingContext { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel?.ViewCreated();
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();
            ViewModel?.ViewAppearing();
        }

        public override void ViewDidAppear()
        {
            base.ViewDidAppear();
            ViewModel?.ViewAppeared();
        }

        public override void ViewWillDisappear()
        {
            base.ViewWillDisappear();
            ViewModel?.ViewDisappearing();
        }

        public override void ViewDidDisappear()
        {
            base.ViewDidDisappear();
            ViewModel?.ViewDisappeared();
        }

        public override void PrepareForSegue(NSStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            this.ViewModelRequestForSegue(segue, sender);
        }
    }

    public class MvxViewController<TViewModel>
        : MvxViewController, IMvxMacView<TViewModel> where TViewModel : class, IMvxViewModel
    {
        public MvxViewController()
        {
        }

        public MvxViewController(IntPtr handle)
            : base(handle)
        {
        }

        protected MvxViewController(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
        }

        public MvxViewController(NSCoder coder) : base(coder)
        {
        }

        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}