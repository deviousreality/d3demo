module.exports = {
  root: true,
  env: {
    "node": true,
    "browser": true,
  },
  "extends": ["eslint:recommended", "plugin:vue/essential"],
  parserOptions: {
    "ecmaVersion": 12,
    "parser": "@typescript-eslint/parser",
    "sourceType": "module"
  },
  rules: {
    'no-unused-vars': 'off',
    'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    "quotes": ["error", "single"],
  },
  overrides: [
    {
      files: [
        '**/__tests__/*.{j,t}s?(x)',
        '**/tests/unit/**/*.spec.{j,t}s?(x)'
      ],
      env: {
        jest: true
      }
    }
  ],
  "ignorePatterns": ["src/assets/d3.js", "src/assets/wh-data.js"],
}
