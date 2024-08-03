public interface IGravityAffected<Context> where Context : class
{
    public void HandleGravity(Context context);
}
