const errorHandler = require('../utils/errorHandler');

module.exports.getRandom = async function(req, res) {
  try {
    if (Math.random() > 0.7) {
      throw new Error('We crashed!!!!!');
    }
    res.status(200).json({ random: Math.random(), server: 'NodeJS' });
  } catch (e) {
    errorHandler(res, e);
    process.exit();
  }
};
