Acrylic DNS Proxy (free, open source) does the job. It creates a proxy DNS server (on your own computer) with its own hosts file. The hosts file accepts wildcards.

Download from the offical website

http://mayakron.altervista.org/support/browse.php?path=Acrylic&name=Home

Configuring Acrylic DNS Proxy
To configure Acrylic DNS Proxy, install it from the above link then go to:

Start
Programs
Acrylic DNS Proxy
Config
Edit Custom Hosts File (AcrylicHosts.txt)
Add the folowing lines on the end of the file:

127.0.0.1   *.localhost
127.0.0.1   *.local
127.0.0.1   *.lc
Restart the Acrylic DNS Proxy service:

Start
Programs
Acrilic DNS Proxy
Config
Restart Acrylic Service
You will also need to adjust your DNS setting in you network interface settings:

Start
Control Panel
Network and Internet
Network Connections
Local Area Connection Properties
TCP/IPv4
Set "Use the following DNS server address":

Preferred DNS Server: 127.0.0.1