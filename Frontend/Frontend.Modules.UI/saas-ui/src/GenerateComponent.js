const fs = require('fs');
const path = require('path');
const args = require('minimist')(process.argv.slice(2));

const componentName = args._[0];
const pathArg = args["path"];

if (!componentName) {
  console.error('Por favor, informe o nome do componente.');
  process.exit(1);
}

const baseDir = path.join(__dirname, 'components');
const targetDir = pathArg
  ? path.join(baseDir, pathArg)
  : path.join(baseDir, componentName);

if (!fs.existsSync(baseDir)) fs.mkdirSync(baseDir);
if (!fs.existsSync(targetDir)) fs.mkdirSync(targetDir);

const jsxPath = path.join(targetDir, `${componentName}.jsx`);
const cssPath = path.join(targetDir, `${componentName}.scss`);

if (fs.existsSync(jsxPath)) {
  console.error('O componente já existe.');
  process.exit(1);
}

const jsxTemplate = 
`import React from 'react'${!pathArg ? `;\nimport './${componentName}.scss'` : ''}

export function ${componentName}() {
  return (
    <section className="${componentName.toLowerCase()} section" id="${componentName}">
      <h1>${componentName}</h1>
      <div className="container">
        
      </div>
    </section>
  );
}
`;

fs.writeFileSync(jsxPath, jsxTemplate);

if (!pathArg) {
  fs.writeFileSync(cssPath, '');
}

console.log(`✅ Componente ${componentName} criado em: ${jsxPath}`);