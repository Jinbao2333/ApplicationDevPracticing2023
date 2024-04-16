using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApp0703 {
    internal class SensitiveWordFilter {
        private TrieNode root; // Trie 树的根节点
        private char maskChar = '*'; // 替换敏感词的字符

        // Trie 树节点
        private class TrieNode {
            public Dictionary<char, TrieNode> Children;
            public bool IsEndOfWord;
            public TrieNode FailNode; // 失败指针

            public TrieNode() {
                Children = new Dictionary<char, TrieNode>();
                IsEndOfWord = false;
                FailNode = null;
            }
        }

        public SensitiveWordFilter() {
            root = new TrieNode();
        }

        // 向 Trie 树中插入一个敏感词
        public void AddWord(string word) {
            TrieNode node = root;
            foreach (char c in word) {
                if (!node.Children.ContainsKey(c)) {
                    node.Children[c] = new TrieNode();
                }
                node = node.Children[c];
            }
            node.IsEndOfWord = true;
        }

        // 构建 AC 自动机的失败指针
        public void BuildACAutomation() {
            Queue<TrieNode> queue = new Queue<TrieNode>();
            queue.Enqueue(root);

            while (queue.Count > 0) {
                TrieNode current = queue.Dequeue();

                foreach (var kvp in current.Children) {
                    char c = kvp.Key;
                    TrieNode child = kvp.Value;

                    // 设置当前子节点的失败指针
                    if (current == root) {
                        child.FailNode = root;
                    }
                    else {
                        TrieNode failNode = current.FailNode;

                        while (failNode != null) {
                            if (failNode.Children.ContainsKey(c)) {
                                child.FailNode = failNode.Children[c];
                                break;
                            }
                            failNode = failNode.FailNode;
                        }

                        if (failNode == null) {
                            child.FailNode = root;
                        }
                    }

                    // 将子节点加入队列，以继续构建失败指针
                    queue.Enqueue(child);
                }
            }
        }

        // 将文本中的敏感词替换为星号
        public string ReplaceSensitiveWord(string text) {
            StringBuilder result = new StringBuilder(text);
            TrieNode node = root;

            for (int i = 0; i < text.Length; i++) {
                char c = text[i];

                if (node.Children.ContainsKey(c)) {
                    node = node.Children[c];
                    if (node.IsEndOfWord) {
                        // 替换敏感词为星号
                        for (int j = i - node.Children.Count + 1; j <= i; j++) {
                            result[j] = maskChar;
                        }
                        // Reset node to the root to continue searching for more sensitive words
                        node = root;
                    }
                }
                else {
                    node = root;
                }
            }

            return result.ToString();
        }

    }
}
