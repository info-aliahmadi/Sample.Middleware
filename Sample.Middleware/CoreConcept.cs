
    void Main()
    {
        Action<string> pipe = (inputMsg) =>
            Try2(inputMsg, (msg) =>
                Try(msg, (msg2) =>
                    Wrap(msg2, Second)));
        pipe("pipe");
    }

    // You can define other methods, fields, classes and namespaces here

    public void First(string msg)
    {
        $"First: {msg}".Dump();
    }
    public void Second(string msg)
    {
        $"Second: {msg}".Dump();
    }
    public void Wrap(string msg, Action<string> function)
    {
        $"Wrap : {msg} ".Dump();
        function(msg);
        "end wrap".Dump();
    }
    public void Try(string msg, Action<string> function)
    {
        $"try: {msg}".Dump();
        function(msg);
        "end try".Dump();
    }

    public void Try2(string msg, Action<string> function)
    {
        $"try2: {msg}".Dump();
        function(msg);
        "end try2".Dump();
    }
