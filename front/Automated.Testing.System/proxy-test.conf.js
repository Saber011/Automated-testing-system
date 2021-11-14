const proxyConfig = require('./proxy-config');

module.exports = proxyConfig({
  backendTarget: 'http://176.126.113.29:5000',
});
