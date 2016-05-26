﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Cinteros.Xrm.FetchXmlBuilder
{
    [Export(typeof(IXrmToolBoxPlugin)),
       ExportMetadata("Name", "FetchXML Builder"),
       ExportMetadata("Description", "Build and test FetchXML with all capabilities supported by the CRM platform"),
       ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAAAFXRFWHRDcmVhdGlvbiBUaW1lAAfgBRoINjvO0UMwAAAAB3RJTUUH4AUaDRMSt1oqIAAAAAlwSFlzAAAK8AAACvABQqw0mAAAANJQTFRFAP8A3uf3rcbnjKXee5zWhKXWpb3n1t73////IVq1AEKtCEqtKWO9OWu9QnPGGFq1a5TO5+/3CEKtxtbv9///zt7vWoTOtc7nGFK1tcbnEEq1lK3eY4zO7/f/9/f/lLXejK3erb3nc5zWvc7v1q3nxozezpze1qXnnLXe7+//79b3pUrGvXvW58bv//f/vXPW3rXnrVrO9+//MWu9SnvGrWPO587vpVLG79739+f33sbvtWvOUoTGc5TWEFK11uf3MWO9ztbvxoTWxrXn3r3vUnvGQwjnpQAAAAF0Uk5TAEDm2GYAAAHoSURBVHjafVPpepswEAQCeJFA5TCEK4Sa2nUip0mc07Fxjjbv/0pZSSVAna/zx/40w+6ONKtpPabR6aws26KptK8Ql3uSsvcdcwhNogNa35GbQgeJYMkIi8e8nb8cwwCvLKyhp8EiZgVjFGE5/RRYoQ0HaPKyq2GQJaxX282Ana820IS14oN9C3DP+bbvsuD8p+iiJi1dZH5dcv7W8VecX4t6jEmDxBCH6zv5lQBWeziXXkiDgpmjjh8fOH8SfzbXnP9RZ2yHAseaWBK/8cNHgGqLpew8z1Mvs2mFHV5rygSiH5zfreFNDFPkWZYleYA9DDqtnc4c56uFtFOkcgTdqTVkPwXCijIgBRkJbn3N9KDOW4T+14qYAwXP3gu1IEmkgO4Q8VgQ1NaOxij4TwtIa+ZrRlgNh7wcDAn7AocMSNQJlM0V53MoaNv6jDQEo+WahRJcDC4qwqGSNlrmGE/Lqc66q77vrvpJ1fQS8dp02T3WVf9YwgpERIbXd06UgcXwudf4e3Mr83CWlgDneMt9oNDKBYBFdRWphtRoYT6M7QKjgUnsQnsqFP/iO531uS9IeTKmK5OYg8XQjlPHng74zM3t8WoFfuiYkdLERy5J9MPtNHFrXY+5lHzzJ1/ut6YbR6Zp2ZNh8w+gCkWngdwxYQAAAABJRU5ErkJggg=="),
       ExportMetadata("BigImageBase64", "iVBORw0KGgoAAAANSUhEUgAAAFAAAABQCAMAAAC5zwKfAAAAFXRFWHRDcmVhdGlvbiBUaW1lAAfgBRoINjvO0UMwAAAAB3RJTUUH4AUaDRIsbyAGygAAAAlwSFlzAAAK8AAACvABQqw0mAAAANtQTFRFAP8A7+/3tc7nlLXec5TWUnvGQnPGOWu9Y4zOhKXWrcbn1uf3pb3nWoTOIVq1AEKtEEq1SnvGjK3e1t739/f/nLXe3uf3CEqtKWO9rb3nlK3ec5zWvc7vSnPGxtbv////GFK1MWO9e5zWa5TOEFK19///7/f/MWu9tcbn79737+f35+/359b3tWPOpUrGtWvW9+//zpzerWPO587vzt7vpVLGvXvW//f/3rXnxozevXPWxpTe1q3nrVrO58bvxoTWGFq13r3vrVLGztbv79b3zqXn9+f31qXnzpTeXEw7XgAAAAF0Uk5TAEDm2GYAAAXcSURBVHjavVl5f+I4DC1bIISShKQh4Yw5SgulpZTpAfScdtqd7/+JVrIdx85F2Nnfvn8mDfaLrCfJsufo6H/GYqYb2rtvId5Hdq91+gdk7mxiWgmcVVqLf0X3WWtaWRh03APZpnWNz/W1mt4JAs/7DOrH5cGQvx7qpUP46m02TdMDl6jwWhXmUV8/L0p3WqUzTN0jqXAbbMB7o9hqdWqBVp+SbJwadJBdQB6POq89I3vgGVTy+j6+2Rm6pzvdxwcIqKN701y+Lo6pegXo0Jdjuuy8CKIj9CLmMfQxirTsACrjcvd6T/HkCKaMshh19PLdIXywbIygi/RVtzD2CrpPYkS17TRlAoiss1M26Gv38GMP0c/bzYo+TNHGcpJvAf71Azrke70E5DPewoiXK/Z59GMyHgfwtsMGb5Fv+XGZZx8bwmxEW5pejO8Y+CZ89A0dvbxeZfJd3bMhfBV9DN7YgqH2jcLCwixcLh+y+L5f+IhwERi/LYWwBm9EwFzy7y9vM4TlS1huwzdTcONQrmafmJTRjFc+YfkzlfCN/3oT1coACMYSoY0fkKY88yn3Vyl8X/zHl2/pJUSjHyXMHfB3lUnbtEmq+fe/5LeeL5sIHnxXS/3jLrkshvl9ujsmkBZhBpaAXY/NW63jjo+9/4pN8CShu7h+kmXJc6rl2/h4MohiEYq+kfS9kPo1xbe7R9LvcMw+qZ51MJGlywKeoAiWAhUlIfU8of56RRx549caEJ2QG05YtnyXuENLRZU8RdNjAsMnyuroMo0cmxLCQ4U5VYEZOYzn4PxDcgIQNm2K6rtF86wBhtG6CA2RwwjNdoSRI0n6WxGYygSEWuiJvo+Jhq77xKDBfxlhkJA6tGkDAj/w5ycSJ8TVVgkxWeBg7XEzCMmPUOor4dGHxyQh7EZtQiqWVWOVsE2yCIWuL18xhRRCB12ORQwjsQfiZBMKwzhEHVcIx9QoCBeTiVzLIRSuU2p0jLBKUwOc5wMhrHycQ0hW1xLfhiQJ3TuMyT4hWLSmNPEcTjhuhZDbh8sPwfebxAirzSZtnP0Wp/AUQgmysUJqeZOhhAuRKN4+QtlEUSZ4xESEUb6iiUnCaiVETaqropCFMS0t2fU8726mY5PgUYoFJSznirKWRHmOEXKgHDrdqVj9zw2bXYGwIRqSzKBesCazmkO4VfgyAhuzzsDAbrNyaJL9qXfLH67TUo/YaCG8GmAbB0znWYSv0VK3qtQqYRuzY8DaunMW5unlKxR4IzkzpXyhUQ0y5PveiG6iaYRC4Fvlrw0nNFlWHet4WPNLJ8BwgoQTqkoKYWILuBfrT+wpYBPsxkO6p9TptpxCGHpNdA+vktQ9hW7oUGEMSngOBrfYjpAucNTf/B1J7dXCrDImTh8+if1HQ/ResOaxNlH6GLFnyh3Ym+oEGa2o/2pgUmcKrDRFotd8ShCCthXeirhnLJ1TBY41RaIb3sQmYOTMpCNerFsSAr/FDfklSy3BZhsKg+crHbEk8C7eHoYnivixA5vqbtQST2g9Swq8TjtZxLOaooo965FiYiVF4DlJQ4rUjfi5QpervjhWvKbyRVKLY0cJ0nikHCBd6EuGC/5zuLc/kwxccqnX4YsKppl6lKKnq6miyJZkgu+DO/4nnutq8cMjJvuY/c5iWt7iEtjIkYMHYzNxBncv0K9sxBU0CzcrkgeQes35PNzz7pIHZnwvhJnPyR5chiMWZuLkyIGWH3aFQe0bxY55EmYWreSH8ZlpgoTA8LTGxa9toIPDiygj+3KpjldkdqkwH+aDVcu7rArwi8OCjvTorZ9+lAvqY8socH3jOrgcf+/1nDuh3dn4fA9fh973Xpzs48MspEP9Xo6VbouN0acF+MDIMbtltTtuKl0wOWN3xUXM4540eF86cGK3ul6jxrvWUb8wHaUsh7fYftsu606j03V6hiautu3D6OjCW1UrA6buHUxHUWoYibv7pu38yX8HAOlfx+VKVUNUanq9uA7/Ff4BNig+IqMbigkAAAAASUVORK5CYII="),
       ExportMetadata("BackgroundColor", "#FFFFFF"), // Use a HTML color name
       ExportMetadata("PrimaryFontColor", "#000000"), // Or an hexadecimal code
       ExportMetadata("SecondaryFontColor", "Blue")]
    public partial class FetchXMLBuilderPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new FetchXmlBuilder();
        }
    }
}