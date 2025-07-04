using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgentumOnline.Net.Commands;

public class UpdateManaCommand : ICommand
{
    public int MinMP { get; set; }

    public void Unpack(Reader reader)
    {
        MinMP = reader.ReadInteger();
    }
}
