namespace Components
{
    public abstract class ComponentBase
    {
        protected bool _hasProgressBar;
        protected ProgressBarComponent _progressBar;
        protected virtual void UpdateProgressBar(float currentValue, float maxValue)
        {
            if (!_hasProgressBar) return;
            _progressBar.UpdateProgressBar(currentValue, maxValue);
        }
        public abstract void UpdateComponent();
    }
}