using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MonoBehaviour : IDisposable

{

    public void Dispose()
    {
        //GC.SuppressFinalize(this);
    }
    virtual public void Start()
    {

    }

    virtual public void Update()
    {

    }
}
