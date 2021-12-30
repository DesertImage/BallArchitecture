using System;

namespace DesertImage
{
    public struct ShowInterstitialAdsEvent
    {
        public Action SuccesCallback;
        public Action CancelCallback;
    }
}