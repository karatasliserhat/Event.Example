
MyEventPublisher publisher = new();
//publisher.XEvent += () => Console.WriteLine("Event Raised");

publisher.XEvent += X;


void X()
{
    Console.WriteLine("Event Tetiklendi.");
}

publisher.RaiseEvent();
publisher.XEvent -= X;

publisher.RaiseEvent();
class MyEventPublisher()
{
    public delegate void XHandler();

    XHandler xDelegate;
    public event XHandler XEvent
    {
        add
        {
            Console.WriteLine("Event Added");
            xDelegate += value;
        }
        remove
        {
            Console.WriteLine("Event Removed");
            xDelegate -= value;
        }

    }

    public void RaiseEvent()
    {
        xDelegate?.Invoke();
    }
}
