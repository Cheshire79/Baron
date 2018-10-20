using Baron.Entity;
using System;

namespace Baron.Strategy.Background
{
	public abstract class AbstractTransition
	{
		public static string SPECIAL_OVERLAY_A1B_0 = "a1b_0";
   // protected BranchActivity context;
		protected TrackImage currentImage;

		public void setContext(//BranchActivity context
			)
		{
			//this.context = context;
		}

		public void destroy()
		{
		//	Log.i(tag(), "destroy");

		}

		public void stopTransition()
		{

		}

		public void dispose()
		{
			//Log.i(tag(), "dispose");
			currentImage = null;
		}

		protected void toggleTransitionContainer()
		{
			//if (context == null) return;

			//View fade = context.getFadeLayoutContainer();
			//View stat = context.getStaticLayoutContainer();
			//switch (getName())
			//{
			//	case TransitionFactory.DEFAULT:

			//		if (stat.getVisibility() != View.VISIBLE)
			//			stat.setVisibility(View.VISIBLE);
			//		if (fade.getVisibility() != View.GONE)
			//			fade.setVisibility(View.GONE);

			//		break;
			//	case TransitionFactory.FADE_OUT:

			//		if (stat.getVisibility() != View.GONE)
			//			stat.setVisibility(View.GONE);
			//		if (fade.getVisibility() != View.VISIBLE)
			//			fade.setVisibility(View.VISIBLE);

			//		break;
			//}
		}

		public void beforeBuild(TrackImage current)
		{

			//if (!BranchPresenter.isCreated()) return;

			//try
			//{

			//	handleSpecialImages(current);
			//	handleSpecialOverlay(current);

			//	OverlayManager manager = BranchPresenter.getInstance().getOverlayManager();
			//	if (manager != null)
			//		manager.start();

			//}
			//catch (Throwable e)
			//{
			//	Log.e(tag(), e);
			//}
		}

		public abstract void build(TrackImage currentId);

		private void handleSpecialOverlay(TrackImage image)
		{
		//	if (context == null) return;

		//	try
		//	{

		//		final ImageView overlay = context.getOverlayComplete();

		//		if (image == null)
		//		{
		//			overlay.setVisibility(View.GONE);
		//			return;
		//		}

		//		switch (image.getId())
		//		{
		//			case SPECIAL_OVERLAY_A1B_0:

		//				if (overlay.getVisibility() != View.VISIBLE)
		//				{
		//					overlay.setVisibility(View.VISIBLE);

		//					ImageManager.loadDrawable(context, "sand", overlay, new RequestListener<Drawable>() {
		//					@Override

		//					public boolean onLoadFailed(@Nullable GlideException e, Object model, Target<Drawable> target, boolean isFirstResource)
		//					{
		//						Log.e(tag(), e);
		//						return false;
		//					}

		//					@Override
	
		//					public boolean onResourceReady(Drawable resource, Object model, Target<Drawable> target, DataSource dataSource, boolean isFirstResource)
		//					{
		//						return false;
		//					}
		//				});
		//		}

		//		break;
		//		default:
  //                  if (overlay.getVisibility() != View.GONE)
		//		{
		//			overlay.setVisibility(View.GONE);
		//		}
		//	}


		//} catch (Throwable e) {
  //          Log.e(tag(), e);
       // }
}

private void handleSpecialImages(TrackImage image)
{
	//if (context == null) return;

	//try
	//{

	//	final LinearLayout blackContainer = context.getBlackCurtains();

	//	switch (image.getId())
	//	{
	//		case "black":
	//			if (blackContainer.getVisibility() != View.VISIBLE)
	//			{
	//				blackContainer.setVisibility(View.VISIBLE);
	//				blackContainer.setAlpha(1);
	//			}
	//			break;
	//		default:

	//			if (blackContainer.getVisibility() != View.GONE)
	//			{
	//				blackContainer.animate()
	//						.setDuration(200)
	//						.alpha(0)
	//						.withEndAction(new Runnable() {
	//								@Override

	//								public void run()
	//				{
	//					blackContainer.setVisibility(View.GONE);
	//				}
	//			}).start();
	//	}
	//}

	//	} catch (Throwable e) {
 //           Log.e(tag(), e);
       // }
    }

   // public abstract ImageView getCurrentVisibleBackground();

protected abstract string getName();

protected abstract String tag();
	}
}
